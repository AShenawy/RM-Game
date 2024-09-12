VAR wonDiscussion = false

-> START
== START ==
I have one friend - a teacher who is very active in using games in everyday teaching practice. I would like to study her and introduce her best practices to the rest of the world. What type of research this is?
+ [Case Study.] -> CaseStudy
* [Survay.] -> Survay
* [Action Research] -> AR
* [Desig Based Research] -> DBR

== CaseStudy ==
Case Study? But isn't this a method about situation scenarios that students are asked to analyse and provide solutions? My focus is on game-based learning.
* [No. You speak about an instructional method. I'm talking about research methodology. They are different things.] -> CaseStudy2
* [You are right. It must be some other strategy.] -> START

== Survay ==
But isn't Survey a methodology for investigating a bigger group of people? I would like to study just one?
* [You are right. It must be some other strategy.] -> START
* [If you are so smart why don't you answer yourself to this question?.] -> Rude

== AR ==
But isn't Action Research a methodology for the teacher if she wants to study herself? What's a suitable methodology for me if I want to study her?
* [You are right. It must be some other strategy.] -> START
* [If you are so smart why don't you answer yourself to this question?.] -> Rude

== DBR ==
Design-Based Research you say? But I don't plan to design anything (except my research). Isnät there any other approach?
* [You are right. It must be some other strategy.] -> START
* [If you are so smart why don't you answer yourself to this question?.] -> Rude

== Rude ==
Hey! There is no need to be so rude. You looked like a helpful person in the first place. 
* [What ever.] -> END

== CaseStudy2 ==
What is your Case Study then?
* [This is in-depth, and detailed examination of a particular case, within a real-world context.] -> Next
* [Case Study is not a serious type of research. It is for weak ones who are afraid of collecting a massive amount of data or doing any other type of proper research.] -> Rude

== Next ==
I see. What should I do next?
* [Select the case.] -> Case
* [Select instruments for data collection.] -> Instruments
* [Prepare data collection and collect data.] -> Instruments2
* [Analyse data.] -> Analyse
*  {CHOICE_COUNT() == 0} [Write a research report.] -> Report

== Case ==
But I have already a case - this one teacher. What's the big deal about selecting the case.
* [Yes but now you need to describe your case.] -> Next
* [Yes but maybe you can consider providing some comparison. Maybe you can study 2 cases?] -> Cases

== Cases ==
What are the typical options for selecting cases?
* [Single case.] -> Single
* [Fork.] -> Fork
* [Best Case.] -> Best
* [Worse Case.] -> Worse
* [Typical Case.] -> Typical
* [Special Case.] -> Special

== Single ==
I believe this is my case.
* [Let's move on then] -> Next

== Fork ==
Fork?
* [2 cases.] -> Cases2
* [2 teachers.] -> Cases2
* [2 classes.] -> Cases2

== Cases2 ==
I'm afraid it's rather difficult for me to find another teacher who is using games in the same manner.
* [The idea is to find a teacher who is using games differently.] -> Next
* [Let's focus on a single case then]. -> Next

== Best ==
I'm sure my teacher is the best case.
* [Let's move on then] -> Next

== Worse ==
Why should I study the worse case?
* [The goal of the research is not only to present and analyse best practices] -> Next

== Typical ==
How can I tell, is my case typical?
* [This is why you need to introduce more than 1 case.] -> Next
* [You may focus on a single case then] -> Next

== Special ==
How can I tell, is my case special?
* [This is why you need to introduce more than 1 case.] -> Next
* [You may focus on a single case then] -> Next

== Instruments ==
This is easy. I plan to use a questionnaire for collecting the data.
* [You plan to send a questionnaire to a single teacher? Why don't you interview her?] -> Instruments2
* [Unfortunately one data collection instrument is not enough. You need to use at least 2 to collect the information from different sources and angles] -> Instruments2 

== Instruments2 ==
What other data collection methods can I use?
* [Questionnaire] -> Next
* [Interview] -> Next
* [Observation] -> Next
* [Document study] -> Next

== Analyse ==
How can I analyse Case Study data?
* [If you have collected quantitative data you can use statistical analysis methods.] -> Statistics
* [If you have collected qualitative data, you can use coding.] -> Coding

== Statistics ==
Sadistiks?! Please no! I don't want to do that!
* [You don't have to. This is your research and you can design it as you like.] -> Analyse
* [Don't be afraid. Give the statistics a chance.] -> Analyse

== Coding ==
Coding!? I don't want to programme anything!
* [No, no! This is describing the text sections with keywords - codes - and then analysing codes - counting them, finding connections, ...] -> Generalisation
* [This country needs programmers. Only some useless member of society says something like you did.] -> Rude

== Generalisation ==
And after analysing data I can interpret results and generalise?
* [Interpretation through discussion yes.] -> Discussion 
* [Generalisation no. You can't make generalizations for the bigger population.] -> NoGeneralisation
* [You can make generalizations trhough theory.] -> Theory

== NoGeneralisation ==
Why can't I make generalizations?
* [Because you studied only a single case.] -> Next
* [Because you are stupid and ask too many questions.] -> Rude

== Theory ==
How can I generalise through the theory?
* [You need to read articles about similar case studies conducted by other researchers and compare your results with them]. -> Next
* [You can analyse more similar cases and compare their results.] -> Next

== Discussion ==
Thank God I'm allowed to do the discussion! The case study seems to be a very restrictive strategy.
* [It is a false assumption that Case Study is easy compared to other research strategies] -> Next
* [Save your tongue until you hear about Triangulation.] -> Triangulation

== Triangulation ==
What's Triangulation?
* [This is about using more than 1 data collection instruments]. -> Instruments2
* [This is about involving more than 1 researchers in interpreting the results]. -> Researcher

== Researcher ==
Why should I hire another researcher?
* [How do you plan to achieve the validity and reliability of your research results?] -> Insane
* [This is just an option commonly used in case studies. You don't need to follow that.] -> Next

== Insane ==
This is insane! Are you trying to kill me with research methods? That's it! I had enough!
* [Ass you wish.]-> END

== Report ==
~ wonDiscussion = true
Yes, every research ends with writing a report. Thank you. 
* [You are welcome!]-> END