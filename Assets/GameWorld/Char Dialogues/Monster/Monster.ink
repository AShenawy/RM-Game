VAR monsterOfferedHelp = false
VAR gotpunkboard = false
VAR completedMinigames = false
VAR firstMeeting = true

/*
Where to start help? 
+ [The player has just entered N2] -> FirstEncounter
+ [The player has completed some mini-games, but not quite enough.] -> SomeButNotEnough
+ [The player has completed both enough mini-games and been to the other dimension, but does not have the board from Punk.] -> NoBoard
+ [The player has completed both enough mini-games and been to the other dimension, where he got board from punk.] -> CompletedMiniGames
*/

{
    // The player has just entered N2
    - firstMeeting: -> FirstEncounter
    
    //### BOTH cases lead to same knot as board is not acquired in game
    // The player has completed both enough mini-games and been to the other dimension, but does not have the board from Punk.
    // The player has completed both enough mini-games and been to the other dimension, where he got board from punk.
    - completedMinigames: -> CompletedMiniGames
    
    // The player has completed some mini-games, but not quite enough.
    - else: -> SomeButNotEnough
}

=== FirstEncounter ===
A perfect someone to play Phutball with me.
* [Sure, sounds fun] -> Acceptance
* [What is Phutball?] -> WhatIs
+ [I don't really have time for games] -> Go

=== Acceptance ===
Great, oh but I forgot Punk has borrowed my board and we will need some counters to play.
* [Where do I get the board from?] -> Punk
* [Where do I get the pieces?] -> WhereToStart
* [Who are you, anyway?] -> WhoAre

===WhatIs===
A game of my own creation, some Philosophers Football.
* [Ok that sounds fun] -> Acceptance 
* [Who are you?] -> WhoAre
+ [I don't really have time for games] -> Go

=== WhoAre ===
My friend Punk calls me Monster. It is a pleasure to meet you. 
* [That’s a strange name.] ->Strangename
* ["Punk? Who is that"?] -> Punk
+ [I'd better go.] -> Go

=== Strangename ===
Well I suppose that is what happens when you don't believe the math is going to work. It was Monsterous Moonshine but You should always trust the Math. So Punk calls me monster because I finally recognized the moonshine. 
* [Well let’s get the things together for Phutball.] -> Acceptance
* [Can you help me?] -> WhereToStart
+ [Whatever you say. I'll get going.] -> Go


=== Punk ===
Punk has a board. She's over in Qualitivile. She is a great conversationalist if a little obsessed with travel. 
+ [Why does she have your board?] -> Board
+ [Whatever you say. I'll get going.] -> Go

=== Board ===
Oh she is working on her own game. I am not sure she has thought through the mathematics of it yet though. It's not really her speciality.
* [ Well I'll go get the board then] -> END

=== WhereToStart ===
There are games and puzzles all around you. Give them a try and they will unlock pieces for us. 
+ [Great see you later.] -> END
+ [You seem to love games] -> GameInfo

=== GameInfo ===
Well yes. You could say that games are life or at least can simulate it. Combinatorial game theory is fascinating.
+[Well I should be going] -> Go

=== Go ===
Yes, good day.
-> END


//---------------------
//SOME MINI-GAMES LATER
//---------------------

=== SomeButNotEnough ===
Oh great you have some of the pieces we still need a couple more however.
+ [Okay.] -> END
* [Can't we just use something else?] -> JustGive

=== JustGive ===
I suppose we could but it would not be nearly as satisfying now would it?
* [Why do they call you Monster again?] -> Strangename
+ [I suppose so.] -> Accept

=== Accept ===
Great when you get back we can start to play.
-> END

//---------------------
//Finished MINI-GAMES but no board
//---------------------

=== NoBoard ===
I can see you have collected enough pieces but we still don't have a board.
+ [How do we get te board?] -> Punk
+ [Can't we just use something else?] -> JustGive


//---------------------
//Finished MINI-GAMES LATER
//---------------------


=== CompletedMiniGames ===
Wonderful you have my board back from Punk and enough of all the pieces shall we start to play?
+ [Yes I am looking forward to it.] ->Fin
+ [What are the rules for Phutball] -> Rules

===Rules ====
I will explain once the board is all setup.
+ [I Suppose that makes sense] -> Fin

=== Fin === 
Just let me get the pieces setup on the board. 
-> END