//A special mob with permission to live before the round starts
/mob/living/carbon/human/lobby
	name = "Glitch in the Matrix"
	status_flags = GODMODE
	//no griff
	density = FALSE

	//we handle this
	notransform = TRUE

	var/new_poll = FALSE

	var/prefers_observer = FALSE
	var/spawning = FALSE
	
	var/mob/living/carbon/human/new_character
	var/obj/screen/splash/splash_screen
	var/datum/callback/roundstart_callback
	
	var/datum/action/lobby/setup_character/setup_character
	var/datum/action/lobby/show_player_polls/show_player_polls

	//"Start Now" memes
	var/said_yes_to_the_dress = FALSE

INITIALIZE_IMMEDIATE(/mob/living/carbon/human/lobby)

/mob/living/carbon/human/lobby/Initialize()
	. = ..()

	GLOB.alive_mob_list -= src
	GLOB.lobby_players += src

	forceMove(locate(1, 1, 1))	//temporary

	equipOutfit(/datum/outfit/vr_basic, FALSE)

	setup_character = new
	setup_character.Grant(src)

	verbs += /mob/dead/proc/server_hop

	roundstart_callback = CALLBACK(src, .proc/OnRoundstart)

/mob/living/carbon/human/lobby/Destroy()
	QDEL_NULL(setup_character)
	QDEL_NULL(show_player_polls)
	LAZYREMOVE(SSticker.round_start_events, roundstart_callback)
	QDEL_NULL(roundstart_callback)
	QDEL_NULL(new_character)
	QDEL_NULL(splash_screen)
	GLOB.lobby_players -= src
	return ..()

/mob/living/carbon/human/lobby/proc/PromptStartNow()
	set waitfor = FALSE
	window_flash(client, ignorepref = TRUE) //important shit
	said_yes_to_the_dress = alert(src, "An admin is starting the game. Do you want to join?", "Quick Start", "Yes", "No") == "Yes"

/mob/living/carbon/human/lobby/proc/CheckPolls()
	if(IsGuestKey(src.key) || !SSdbcore.Connect())
		return
	var/client/C = client
	var/datum/DBQuery/query_get_new_polls = SSdbcore.NewQuery("SELECT id FROM [format_table_name("poll_question")] WHERE [(C.holder ? "" : "adminonly = false AND")] Now() BETWEEN starttime AND endtime AND id NOT IN (SELECT pollid FROM [format_table_name("poll_vote")] WHERE ckey = \"[C.ckey]\") AND id NOT IN (SELECT pollid FROM [format_table_name("poll_textreply")] WHERE ckey = \"[C.ckey]\")")
	new_poll = query_get_new_polls.Execute() && query_get_new_polls.NextRow()

/mob/living/carbon/human/lobby/proc/MoveToStartArea()
	if(loc)
		RunSparks()
	forceMove(get_turf(pick(SSticker.lobby.spawn_landmarks)))
	RunSparks()

/mob/living/carbon/human/lobby/proc/IsReady()
	return (client || new_character) && (said_yes_to_the_dress || istype(get_area(src), /area/shuttle/lobby/start_zone))

/mob/living/carbon/human/lobby/proc/OnInitializationsComplete(immediate = FALSE)
	set waitfor = FALSE
	if(!immediate)
		var/obj/docking_port/mobile/crew/shuttle = SSshuttle.getShuttle("crew_shuttle")
		UNTIL(shuttle.mode == SHUTTLE_CALL)	//let the shuttle roundstart dock
	QDEL_NULL(setup_character)
	QDEL_NULL(show_player_polls)
	window_flash(client, ignorepref = TRUE) //let them know lobby has opened up.
	PhaseOutSplashScreen()
	MoveToStartArea()
	if(!new_poll)
		return
	for(var/I in SSticker.lobby.poll_computers)
		var/obj/machinery/computer/lobby/poll/comp = I
		client.images += comp.new_notification

/mob/living/carbon/human/lobby/proc/OnRoundstart()
	if(!new_character)
		return
	
	var/mob/nc = new_character
	nc.notransform = TRUE
	addtimer(VARSET_CALLBACK(new_character, notransform, FALSE), 30, TIMER_CLIENT_TIME)
	transfer_character()
	PhaseOutSplashScreen(nc)

/mob/living/carbon/human/lobby/proc/PhaseOutSplashScreen(mob/character)
	splash_screen.Fade(TRUE, character != null)
	if(character)
		splash_screen = null
	else
		notransform = FALSE

/mob/living/carbon/human/lobby/proc/PhaseInSplashScreen()
	invisibility = INVISIBILITY_MAXIMUM
	RunSparks()
	notransform = TRUE
	splash_screen.Fade(FALSE, FALSE)

/mob/living/carbon/human/lobby/proc/RunSparks()
	do_sparks(5, FALSE, src)

/mob/living/carbon/human/lobby/proc/PhaseOut()
	RunSparks()
	key = null//We null their key before deleting the mob, so they are properly kicked out.
	qdel(src)
