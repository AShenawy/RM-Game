VAR wonDiscussion = false

Hi traveller! You look like an educated person. Can you help me sort out one thing?
* [How can I help you?] -> Help
* [Sorry I don't have time for this] -> Fin

== Help ==
You see I need to choose a methodology for my final thesis but I have no idea what this thing is or what to choose?
* [That's easy, a methodology is like a strategy that will shape and structure your study.] -> Strategy
* [I'm not sure but I think those are instruments that help you to collect data.] -> Instrument
* [I know what this thing is. It's the research philosophy or way how you understand the truth,] -> Philosophy
* [I'm sorry I have no idea what you are talking about.] -> Fin

== Strategy ==
I see. But what is my strategy if I would like to experiment with games in an educational context?
* [That is an interesting question but be careful while using word EXPERIMENT] -> Why
* [You said experiment. Maybe it is an experiment then.] -> Experiment
* [I'm sorry I do not know.] -> Fin

== Philosophy ==
No that can't be right. My teachers did not tell us anything about research philosophy.
* [Let's see then ...] -> Help
* [I'm sorry, then I don't know.] -> Fin

== Instrument ==
No No. My supervisor says instruments are methods. This is a different thing.
* [Let's see then ...] -> Help
* [I'm sorry, then I don't know.] -> Fin

== Why ==
Why can't I use the word Experiment?
* [You can but Experiment is a very specific research strategy] -> Experiment
* [It's not okay to experiment with students. Education is a serious business.] -> Fin

== Experiment ==
What exactly the experiment is then?
* [Experiment is about finding the connection between the cause and the effect.] -> Cause
* [Experiment is testing how the independent variable influences the dependent variable.] -> Independent

== Cause ==
So if I would like to experiment with learning games what could be the cause and effect?
* [For example what is the impact of using such a game on learning results?] -> Next
* [For example how to make such a game?] -> NotRight
* [For example how students perceive this game?]  -> NotRight

==  NotRight ==
This doesn't sound right.
* [Let's see then ...] -> Cause
* [I'm sorry, then I don't know.] -> Fin

== Independent ==
So if I would like to experiment with learning games what can be the independent variable and what is the dependent variable?
* [For example independent variable is the time how long learners use your game. The dependent variable is the test result.] -> Next
* [Independent variable is the game, the dependent variable is the learning result.] -> TooGeneral

== TooGeneral ==
Isn't this too general?
* [Let's see then ...] -> Independent
* [I'm sorry, then I don't know.] -> Fin

== Next ==
Wery well then. What should I do next?
* [Create test and control groups.] -> Group
* [Plan group activities.] -> Activity
* [Measure the impact.] -> Measure
* {CHOICE_COUNT() == 0} [Write a research report.] -> Report

== Group ==
What do you mean by test and control groups?
* [You need to divide your subjects between 2 groups a test group and a control group.] -> TooMuch
* [Test group is using a learning game.] -> Next
* [Control group is not using the game.] -> What

== What ==
If they are not using the game what they should do?
* [They can learn through some traditional teaching methods like presentations.] -> TooMuch
* [They can learn through some other active learning methods like problem-based learning.] -> TooMuch

== Activity ==
What do you mean by group activities?
* [In a test group you use learning games and you test your students.] -> TooMuch
* [In a control group you use some other teaching methods and run the same test.] -> TooMuch

== Measure ==
How can I measure the differences?
* [The typical data collection method in thr Experiment is a Test] -> Test
* [You will run a pre-test and post test] -> PrePost

== Test ==
What will I test?
* [You can test the knowledge of your students.] -> Next
* [I don't know, it depends on your Hypothesis] -> Hypothesis

== Hypothesis ==
Hypothesis?
* [Yes, a hypothesis is a supposition as a starting point for further investigation.] -> TooMuch
* [For example the use of learning games increases the students' learning results.] -> Next

== PrePost ==
Pre- Post- what?
* [You will test your students' knowledge before the experiment and after the experiment with the same test.] -> TooMuch
* [Take a vocabulary and check the meaning of the pre and post.] -> Fin

== TooMuch ==
This is too much work. I understand in real science you need to do that but at school? Can it be somehow easier ... less work?
* [You can run Quasi Experiment.] -> Quasi
* [No school is also real life. How can you practice for real life if you don't try that at school?] -> Next

== Quasi ==
What is Quasi Experiment?
* [This is almost an experiment.] -> Next
* [This is an experiment with no control group.] -> NoControl
* [This is an experiment with no pre-test.] -> NoPre

== NoControl ==
But the test group is still needed?
* [Yes] -> Next

== NoPre ==
But the post-test is still needed?
* [Yes] -> Next

== Fin ==
Very well then I'll try to solve my issues by myself. 
* [This is a right decision.] -> END

== Report ==
~ wonDiscussion = true
Yes, every research ends with writing a report. Thank you. 
* [You are welcome!]-> END