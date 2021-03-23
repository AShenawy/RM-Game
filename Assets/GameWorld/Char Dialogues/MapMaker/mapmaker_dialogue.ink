VAR usedCrystal = false
VAR reminderCrystal = false
VAR offeredHelp = false
VAR gotStudentCoin = false

Where to start?

+ [The player has just entered N1-Qual after using a crystal]:
~ usedCrystal = true
-> FirstEncounter

+ [The player has just entered N1-Qual without using a crystal]:
~ usedCrystal = false
-> FirstEncounter

+ [The player has completed some mini-games, but not quite enough.] -> SomeButNotEnough

+ [The player has completed enough mini-games in the qualitative dimension, but has not visited the other dimension.]:
~ usedCrystal = false
~ gotStudentCoin = false
-> Ready

+ [The player has completed both enough mini-games and been to the other dimension, but does not have the coin from Student.]:
~ usedCrystal = true
~ gotStudentCoin = false
-> Ready

+ [The player has completed both enough mini-games and been to the other dimension, where he got a coin from Student.]:
~ usedCrystal = true
~ gotStudentCoin = true
-> Ready

=== FirstEncounter ===
A strange place, is it not? I have been trying to make a map of it, to understand what these people believe, how they govern themselves, what their work ethic is. I need to collect more data to analyze, but I have my hands full with this map.

* [Can I help?] -> HelpYou
* [Who are you?] -> WhoAre
+ [Em- good luck, I guess.] -> Luck

=== HelpYou ===
~offeredHelp = true
Hmm, I suppose you could. I need to know more about this place to form an interpretation of its society. If you could use a variety of data collection methods to learn more about it and report what you have discovered back to me, I can help you past that gate over there.

* [We've got a deal.] -> Go
* [Okay, but where do I start?] -> WhereToStart
* [Who are you, anyway?] -> WhoAre

=== WhoAre ===
A sociologist, a fencer, a Protestant... you can call me Mapmaker: that is what the beer-drinking statistician calls me. Not that he is of much use, with his numbers and formulas. You cannot understand society by looking at it is if it were a mathematical formula!

* [Why not?] ->WhyNot
* ["The beer-drinking statistician"?..] -> Statistician
+ [I'd better go.] -> Go

=== Statistician ===
The fellow in the other dimension, calls himself Student. Have you visited him already? {usedCrystal: 
Those crystals are quite handy, are they not? 
-else:
You may want to use that crystal of yours, unless you fancy staying here forever in my company... which I certainly would rather avoid, what with all the work I have to do.
}
* [Can I help you with your work?] -> HelpYou
* [Can you help me?] -> HelpMe
+ [Alright, I need to go.] -> Go

=== WhyNot ===
Because numbers do not explain how a society works! Societies are not mathematical phenomena to be measured; we must learn about what people do and interpret that information to understand them. This is where Student and I disagree. I suppose his work has its uses, just not for what I am trying to do here.
* [Can I help?] -> HelpYou
* [Can you help me?] -> HelpMe
+ [Whatever you say. I'll get going.] -> Go


=== HelpMe ===
Not unless it's worth my time. I am too busy working on this map. Even this conversation has been taking too long, if I am being frank.
* [Maybe I could do something for you?] -> HelpYou
+ [I'll get going, then.] -> Go

=== Student ===
The statistician, he spends his time in the other dimension. I do not agree with him much, but we have had some good debates. Click on your crystal when you feel like visiting him.
+ [I will.] -> Go

=== WhereToStart ===
Just look around. There are many things waiting to be discovered. If I were you, I would stop dilly-dallying and would get to it already. 
+ [Okay, okay.] -> Go

=== Luck ===
We shall see which one of us needs luck. But yes, good day.
-> END

=== Go ===
{not offeredHelp:
 Pray do. But unless you develop a more inquisitive mind, you may only be able to get so far.
}
{offeredHelp and not usedCrystal and not reminderCrystal:
~ reminderCrystal = true
Oh, and do make use of that crystal to visit Student. You will probably need his help to get past the gate too.
}
{offeredHelp and (usedCrystal or reminderCrystal):
{cycle:
- Good day.
- Yes, yes, begone now.
- I shall see you later, possibly.
}
}
* {offeredHelp and not usedCrystal and not reminderCrystal} [Who's Student?] -> Student
// Why on earth is this throwing a warning and not showing the choice???
-> END

//---------------------
//SOME MINI-GAMES LATER
//---------------------

=== SomeButNotEnough ===
I can see you have been making progress in surveying this land. So what can you tell me about this place? What is their religion? Where are the bureaucrats? You do not know? Maybe you should look around more and come back when there is something meaningful for us to discuss.
+ [Okay.] -> END
* [Why can't you just give me what I want?] -> JustGive
* [Can you not do it yourself?] -> DoYourself

=== JustGive ===
That is the ethic I was raised in. If one wants to get something, they must make an effort and earn it first. Also, I do need those data and there isn't exactly a long line of needy youths in front of me whom I could recruit to help.
* [Can you not do it yourself?] -> DoYourself
+ [I understand.] -> Accept

=== DoYourself ===
I could, but who would be working on the map while I am collecting the data? My nation loves nothing more than efficiency. Division of labour is a big part of that.
+ [Makes sense.] -> Accept

=== Accept ===
I am glad that you understand. Or pretend to, which is good enough for my purposes. Go and finish collecting those data now!
-> END

//---------------------
//MINI-GAMES ARE DONE
//---------------------
=== Ready ===
So what did you learn about this place? ...I see. Well, you have been somewhat useful, even if it falls to me to interpret your findings and understand this society. Here, this coin will take you past the gate.
* [Thank you!] -> Thanks
* [So how long have you been here?] -> BeenHere
* [Are you always this sour?] -> Sour

=== Sour ===
Wouldn't you be, stuck in this place with otherwordly strangers asking you favours while you are trying to work? I am in one of my better moods right now, actually.
{not gotStudentCoin:
<> Oh, and one more thing.
*[What?]->Thanks
}
* {gotStudentCoin}[Whoa. Okay then. Ciao!] -> END

=== BeenHere ===
Too long to keep count. Probably longer now than in the world I had come from. But it's a strange place, and time does not mean much here. In the years I have wandered it, it does not seem to have changed much.
{not gotStudentCoin:
<> Oh, and one more thing.
*[What?]->Thanks
}
* {gotStudentCoin}[Strange indeed. Bye!] -> END

=== Thanks ===
{
-gotStudentCoin == 1:
Think nothing of it.
-usedCrystal==1 and gotStudentCoin==0:
Don't run off just yet! You'd need another coin from Student in the other dimension to pass through the gate. He doesn't have as nice a collection as I do, but I believe that if you assist him, he will be able to give you one.
-else:
You still do not seem to have visited the other dimension. How can you learn about one society without another to compare it to? I suggest you use that crystal of yours to visit Student in the other dimension. You would need to get another coin from him to get past the gate anyway.
}
-> END

