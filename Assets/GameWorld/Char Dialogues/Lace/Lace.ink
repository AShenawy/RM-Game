VAR offeredHelp = false
VAR completedMiniGames = false
VAR firstMeeting = true


{
    // The player has just entered N3
    - firstMeeting: -> FirstEncounter

        // The player has completed enough mini-games.
    - completedMiniGames: -> CompletedMiniGames
    
    // The player has completed some mini-games, but not quite enough.
    - else: -> SomeButNotEnough
    

}

/*
=== START ===
Where to start help? 
+ [The player has just entered N3] -> FirstEncounter
+ [The player has completed some mini-games, but not quite enough.] -> SomeButNotEnough
+ [The player has completed enough mini-games.] -> CompletedMiniGames
*/

=== FirstEncounter ===
So it rained at Cheltenham yesterday which will make it soft going. Oh you must be one of Monster’s friends.
+ [Why do you think Monster and I are friends? ] -> ClothingQuery
+ [Of course I am, we just finished playing Phutball.] -> Monster
+ [What is this machine?] -> Engine
+ [Who are you?] -> WhoAre

=== Engine ===
An invention of my good friend Babbage. 
+ [How does this thing work?] ->Work
+ [What can you do with this Engine?] ->EngineDo

=== Work ===
It is supposed to be able to calculate variables at a great speed. If I can get the cards sorted to tell it what to do that is.
+ [I can help you get the cards sorted out.] -> Acceptance
+ [How do we get the cards sorted?] -> WhereToStart
+ [What’s in it for me?] -> Challenge

=== EngineDo ===
This engine can calculate numbers, find answers and solve problems. In theory it can pick the winning horse in a race, but sadly the variables are quite confounding my dear, I mean the state of the track alone was a problem I was pondering when you arrived. However It would be very simple for it to open that bunker door.
+ [I'm not interested in gambling.] -> Challenge
+ [Let's open the bunker door.] -> Acceptance

=== Monster ===
He is a charming gentleman although a nightmare to play cards with, I swear he counts the cards. But maybe if you are a friend of his you can help me get this metal behemoth to perform its function?
+ [Sure. I would be happy to.] -> Acceptance
+ [What is the machine?] -> Engine
+ [What’s in it for me?] -> Challenge
	

=== Challenge ===
Well I assumed you were following the lady in the same style of clothing as you.
+ [Which way did she go?] -> Supervisor
+ [Oh. Have you seen her?] -> Supervisor

===ClothingQuery===
Well you are in the same funny Garb. So of course you must be friends. Just like that strange lady that came through recently.
+ [Which way did she go?]-> Supervisor
+ [What do you think of Monster?] -> Monster
+ [I don’t have time for this, I must go!] -> END

===Supervisor===
A dishevelled lady dressed like you? Why, yes! she closed that bunker door behind her and I heard it lock.
+ [How do I get the door open?] -> EngineDo
+ [Who are you?]-> WhoAre

=== Acceptance ===
Well my fine Knight Errant we need to gather the cards that tell our mechanical mind here how to think.
+ [Huh?] -> WhereToStart
+ [Who are you, anyway?] -> WhoAre

===WhereToStart===
I would have thought you had figured the pattern out by now. Engage with the challenges you find scattered around, defeat them like a good Galahad and return to me.
+ [Knight Errant, Galahad????] -> ClassicalEducation
+ [Oh ok then] -> END

====ClassicalEducation====
Is no one reading the Classics anymore. No Homer, No Chaucer, my my how father would weep. More than a third of your life must truly have been spent in sleep. I mean that you are on a quest and have proven your worth before this. 
+ [I see I will get started right away.] -> END
+ [So how exactly do I get started.] -> WhereToStart

=== WhoAre ===
The countess, Number enchantress, Mistress of the Race, But you, may call me Lace.
+ [Well let's get started.] -> Acceptance
+ [That was very Poetic.] -> Strangename
+ [Enough rhyming time for action] ->END


=== Strangename ===
A gift from my father, I suppose.  But if I am to place my bets at Cheltenham we should get started.
+ [What do I need to do?] -> Acceptance
+ [Whats in it for me] -> Challenge
+ [Ok I will get started] -> END



//---------------------
//SOME MINI-GAMES LATER
//---------------------

=== SomeButNotEnough ===
Almost there. We just need a few more cards punched to complete the algorithm.
+ [Okay.] -> END
+ [Can't we just use something else] -> JustGive

=== JustGive ===
I suppose we could use some gunpowder. A sufficient charge would solve our problems.
+ [Gunpowder you say.] ->Gunpowder
+ [Why do they call you Lace again?] -> Strangename
+ [I suppose so.] -> Accept

=== Gunpowder ===
Although I am 90% sure it would kill whoever is inside the building.
+ [Let's do it!] -> Disappointed
+ [Well I will get back to the cards then] -> Accept

=== Disappointed ===
That is not the noble Galahad you should aspire to be.
+ [Galla who?] -> ClassicalEducation
+ [Very Well] ->Accept

=== Accept ===
When you have completed your tasks we will get you on your travels
-> END

//---------------------
=== CompletedMiniGames ===
Well my Hercules returns having completed the arduous tasks!
+ [Yes, Yes let’s get this door open.] ->Fin
+ [Hercules HuH??] -> Hercules

===Hercules====
I mean really what is modern education coming to? You have proven yourself worthy and stand on the verge of achieving your goal. 
+ [I Suppose that makes sense] ->Fin


=== Fin === 
Very well stand back as I engage the steam turbines -> END
