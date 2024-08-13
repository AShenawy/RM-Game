VAR monsterOfferedHelp = false
VAR N2QlProgress = 0
VAR gotpunkboard = 0
VAR PunkEncountered = 1
VAR on = 0

/*
(This bit won't be in the actual game.)

Before you start talking to Punk, tell me this: did you visit Monster in Quantiville, and did he ask you to retrieve his board from Punk?

+ [I did, and he did.] -> punkPrep1
+ [No, I did not meet Monster.]  -> punkPrep3
+ [No, Monster did not ask me to retrieve his board.]  -> punkPrep3

=== punkPrep1 ===
~ monsterOfferedHelp = true

-> punkPrep2

=== punkPrep2 ===
And how many minigames have you completed in Qualiville 2?
+ [None.] -> punkPrep3
+ [Some, but not enough.] -> punkPrep3_notenough
+ [Enough.] -> punkPrep3_enough

=== punkPrep3_notenough ===
~N2QlProgress = 1
->punkPrep3

=== punkPrep3_enough ===
~N2QlProgress = 2
->punkPrep3

=== punkPrep3 ===
And finally, is this the first time you are talking to Punk?

+ [Yes.] -> punkPrep4_a
+ [No, we have conversed before.] -> punkPrep4

=== punkPrep4_a ===
~PunkEncountered = 1
-> punkPrep4

=== punkPrep4 ===

Thanks! Now you can start the dialogue as it will be in the actual game.
+ [Start.] -> PunkEncounter
*/

-> PunkEncounter

// DIALOG PROPER STARTS HERE

=== PunkEncounter ===
~ on = 1
{   PunkEncountered == 1:
~PunkEncountered = 1
Why, hello there. It's always exciting to see a new face. What brings you to this wondrous land? 
    -else:
Good to see you again!
}

+ {not PunkIntroduction} [Who are you?] -> PunkIntroduction
+ {not PunkHelp and PunkIntroduction} [Is there anything I can do to help you?] -> PunkHelp
+ {monsterOfferedHelp and not gotpunkboard} [Monster would like his board back.] -> MonsterBoardBackPlease
+ [Why do you have a mask?] -> PunkMask
+ {not PunkBridge} [How can I lower the bridge?] -> PunkBridge
+ {PunkBridge} [About that bridge...] -> PunkBridge
+ [I have to go.] -> PunkBye

=== PunkIntroduction ===
Please call me Punk. That's what my dad called me growing up, and it stuck. I am a scholar of people and of their mores. I have traveled to many places, always coming back with more questions than answers, and now I have found myself here.
+ [Why do you have a mask?] -> PunkMask
+ {not PunkHelp} [Is there anything I can do to help you?] -> PunkHelp
+ {not PunkBridge} [How can I lower the bridge?] -> PunkBridge
+ {PunkBridge} [About that bridge...] -> PunkBridge
+ {monsterOfferedHelp and not gotpunkboard} [Monster would like his board back.] -> MonsterBoardBackPlease
+ [Nice to meet you, Punk, but I must go.] -> PunkBye

=== PunkHelp ===
I appreciate the offer, dear, but I am feeling quite self-sufficient at the moment, and can handle my work and my existence, for the time being at least. But do feel free to have a look around, and I am sure you will find other ways of putting your talents to use.
+ [Why do you have a mask?] -> PunkMask
+ {not PunkBridge} [How can I lower the bridge?] -> PunkBridge
+ {PunkBridge} [About that bridge...] -> PunkBridge
+ {monsterOfferedHelp and not gotpunkboard} [Monster would like his board back.] -> MonsterBoardBackPlease
+ [I have to go.] -> PunkBye

=== PunkMask ===
Don't we all? Don't all of us wear masks most of the time and change them according to whom we are with? This particular one, however, is one of my favorites. I received it as a gift from a fisher on a faraway island. He was very proud of his masks, and they made him who he was. When he gave me one as a gift, though, that mask changed its meaning, becoming a symbol of our bond. Fascinating things, aren't they?
+ {not PunkIntroduction} [What is your name?] -> PunkIntroduction
+ {not PunkBridge} [How can I lower the bridge?] -> PunkBridge
+ {PunkBridge} [About that bridge...] -> PunkBridge
+ {not PunkHelp} [Is there anything I can do to help you?] -> PunkHelp
+ {monsterOfferedHelp and not gotpunkboard} [Monster would like his board back.] -> MonsterBoardBackPlease
+ [I have to go.] -> PunkBye

=== PunkBridge ===
{I'm afraid I don't know, dear. Truth be told, I am more interested to know why there is a bridge here in the first place. It's not like there are scores of travelers seeking to cross the river. There was one, and quite recently, however. I suspect crossing the bridge must be a rite of passage of some kind... Oh, sorry, I got carried away. If you are looking for a way to draw the bridge, you might want to ask Monster about it.|You may want to consult Monster about it. I do not know how the woman I saw earlier managed to lower it. But I am reasonably certain the bridge serves as some kind of a rite of passage.}
+ [Someone crossed the bridge?] -> SomeoneCrossed
+ [What's a rite of passage?] -> RiteOfPassage
+ [Who is Monster?] -> AboutMonster
+ [I'd like to ask you about something else.] -> PunkBasic
+ [Alright, I should go now.] -> PunkBye

=== PunkBasic ===
 {&Of course!|You have an inquisitive mind.|Indeed?} What would you like to know?
+ {not PunkIntroduction} [What is your name?] -> PunkIntroduction
+ {not PunkHelp and PunkIntroduction} [Is there anything I can do to help you?] -> PunkHelp
+ {monsterOfferedHelp and not gotpunkboard} [Monster would like his board back.] -> MonsterBoardBackPlease
+ [Why do you have a mask?] -> PunkMask
+ [About that bridge...] -> PunkBridge
+ [I have to go.] -> PunkBye

=== SomeoneCrossed ===
Yes, a woman who was in such a hurry I didn't even have a chance to ask her name... that's a shame, she looked like someone with stories to tell. There is something about you that reminds me of her, although I cannot quite put my finger on it.
+ [What's a rite of passage?] -> RiteOfPassage
+ [Who is Monster?] -> AboutMonster
+ [I'd like to ask you about something else.] -> PunkBasic
+ [I have to go.] -> PunkBye

=== RiteOfPassage ===
It is a ritual during which one undergoes some kind of transformation: from child to adult, or from student to man or woman of letters — from anything to something else that can be the next stage in their life journey.
+ [Someone crossed the bridge?] -> SomeoneCrossed
+ [Who is Monster?] -> AboutMonster
+ [I'd like to ask you about something else.] -> PunkBasic
+ [I have to go.] -> PunkBye

=== AboutMonster ===
Oh, you haven't met him yet? He's quite a charming fellow, mathematician by trade, over on the Winter Side. I rather enjoy our discussions, but it is so darn cold over there, and he hardly ever visits me here.
+ [What's a rite of passage?] -> RiteOfPassage
+ [Someone crossed the bridge?] -> SomeoneCrossed
+ [I'd like to ask you about something else.] -> PunkBasic
+ [I have to go.] -> PunkBye
=== MonsterBoardBackPlease ===
{
- N2QlProgress == 0:
Oh, I am sure he would. But, see, he did say I could keep the board for as long as I needed it — and I do happen to still need it for a little game I am working on. I hope to be done soon, though. So maybe you can have a little look around and then come see me again in a bit.
- N2QlProgress == 1:
Ah yes, of course. I do need the board for a few more minutes, though. Maybe you could keep yourself busy in the meantime by exploring a bit more?
- else:
Yes, of course. Here it is. Please tell him I am very grateful, and his board helped me a lot with my own project.
~ gotpunkboard = 1 
}

+ [Tell me about the project you needed the board for.] -> PunkAboutGame
+ {not PunkIntroduction} [What is your name?] -> PunkIntroduction
+ {not PunkHelp and PunkIntroduction} [Is there anything I can do to help you?] -> PunkHelp
+ [Why do you have a mask?] -> PunkMask
+ {not PunkBridge} [How can I lower the bridge?] -> PunkBridge
+ {PunkBridge} [About that bridge...] -> PunkBridge
+ [I'd better go now.] -> PunkBye

=== PunkAboutGame ===
Just a little game I started with my third husband many years ago. We never quite finished it, though. We have a lot of unfinished business, him and I. But I would rather not go into it. I should show this game to Monster when it is ready, though. He considers himself an expert.
+ {not PunkIntroduction} [What is your name?] -> PunkIntroduction
+ {not PunkHelp and PunkIntroduction} [Is there anything I can do to help you?] -> PunkHelp
+ [Why do you have a mask?] -> PunkMask
+ {not PunkBridge} [How can I lower the bridge?] -> PunkBridge
+ {PunkBridge} [About that bridge...] -> PunkBridge
+ [I'd better go now.] -> PunkBye


=== PunkBye ===
{&Goodbye!|I will see you soon.}
* [Leave]
-> END
