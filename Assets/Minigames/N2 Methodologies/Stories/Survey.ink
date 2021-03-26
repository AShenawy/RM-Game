VAR wonDiscussion = false

-> START
== START ==
I have one question? What research design I should use if I would like to know how many teachers are using games in educational conditions?
* [I suggest Experiment.] -> Experiment
* [This sounds like a Survey.] -> Survey
* [Case study is always a safe choice.] -> CaseStudy
* [You mentioned design - I suggest Design-Based Research.] -> DBR

== Experiment ==
You think so? But isn't the experiment for discovering a connection between the dependent and independent variables? I don't have this kind of ambition.
* [Maybe then it's something else.] -> START
* [Sorry, then I don't know.] -> END

== Survey ==
You think so? Can you tell a bit more?
* [Well, Survey is a suitable strategy for collecting data from a bigger group of objects in a standardised manner.] -> Standardised
* [Survey is suitable if you have a bigger population.] -> Population
* [It fits if you can quantify your research questions.] -> Quantification

== CaseStudy ==
Case Study you say? But isn't the case based on one or a couple of samples only? I would like to get a bigger picture?
+ [Thren you should do a survey.] -> Survey
* [Maybe an experiment then?] -> Experiment
* [Sorry, then I don't know.] -> END

== DBR ==
But isn't Design-Based Research about developing new things e.g. games? I don't want to do that.
* [You mentioned design. I thought you wanted to make some new things.] -> Design
* [Why not? Making new games sounds like fun.] -> Fun

== Design ==
By research design, I mean what is my research strategy? What is my study framework or so-called methodology?
* [I see. Let's see what are your options again?] -> START
* [Ah, this is too much over my competency level.] -> END

== Fun ==
Yes, but for most serious scholars, this is not decent research. I don't want to take that risk.
* [Maybe then you need to choose something else.] -> START
* [Sorry, then I don't know.] -> END


== Standardised ==
What do you mean by "standardised"?
* [Your study is based on pre defined plan.] -> Plan
* [You use structured instruments for data collection.] -> Instruments
* [You use standardised methods fort data analysis.] -> AnalyseData

== Population ==
What is a population?
* [It is a larger group of individuals or objects that is the main focus of your study.] -> All
* [It is a comprehensive group of individuals, institutions or objects with common characteristics that are the interest of a researcher.] -> All

== Quantification ==
What do you mean by quantification?
* [This is a method of describing your research questions by metrics and numbers.] -> Plan
* [This is the act of counting and measuring human behaviour and experiences in quantities.] -> Plan

== All ==
Do I need to study every individual in my population?
* [No, no! You will create a sample.] -> Sample
* [Yes, ofcourse] -> NotForMe
* [This is why I suggest Case Study instead of Survey] -> CaseStudy

== Sample ==
What is a sample?
* [It is a small part or quantity intended to show what the population is like.] -> Sampling
* [This is one example from the bigger population] -> Example
* [This is a sound or piece of music created by sampling] -> Music

== Sampling ==
What methods I can use for creating the sample?
* [I suggest random sample.] -> RND
* [I suggest systematic sample.] -> Systematic
* [I suggest convenience sample.] -> Convenience
* [I suggest quota sample.] -> Quota

== Example ==
One example? But this sounds like a Case Study!
* [You are right. A sample is something else.] -> Sample
* [If you are so smart why do you need my help?] -> END

== Music ==
This doesn't make any sense.
* [I was joking.] -> Sample
* [But you said sound.] -> Fin

== Fin ==
Hei! You are only teasing me. I think I have had enough of your help. Have a good day.
* Leave
-> END

== NotForMe ==
This is too much. It seems that research is not for me. I quit.
* Leave
-> END

== RND ==
Random sample - does it mean I can put together the group of individuals I study arbitrary?
* [No, randomness means every member of the population has an equal chance to be part of the sample.] -> SampleSise
* [Yes.] -> Arbitrary

== Arbitrary ==
This doesn't sound right.
* [You are right. I was joking] -> RND
* [If you are so smart why do you need my help?] -> END

== Systematic ==
What is a Systematic Sample?
* [This is the method of creating the sample based on the list]. -> SampleSise
* [For example every 5th teacher in the list will be selected to the sample]. -> SampleSise

== Convenience ==
What is a Convenience Sample?
* [It is non-probability sampling.] -> Convenient
* [It involves the sample being drawn from that part of the population that is close to hand.] -> Convenient

== Quota ==
What is a Quota sample?
* [This is a non-probabilistic version of stratified sampling.] -> Stratified
* [First you divide your population into segments, then you select a specific number of subjects from every segment.] -> Convenient

== Stratified ==
What is a Stratified sample?
* [This is creating a random sample based on sub-populations.] -> Difficult
* [...] -> Difficult

== Difficult ==
No, this is too difficult. I need to choose an easier sampling method.
* [Let's go back then.] -> Sample
* [Go home then!] -> Fin

== Convenient ==
This sounds convenient I'll use this sample type.
* [You can do that ...] -> Plan
* [... but then you can't conduct scientific generalisation.] -> Generalisation

== Generalisation ==
What do you mean by generalisation?
* [Can you predict that the things you find in your sample, apply also to the population?] -> Important
* [...] -> Important

== Important ==
I guess generalisation is important.
* [Lets go back then.] -> Sample
* [I'm sure you guess?!] -> Fin

== SampleSise ==
How big should be my sample?
* [This depends on the size of your population.] -> SamleExample
* [This depends on how big is your conficence level.] -> ConfidenceLevel
* [Thsi depemds on how big is your conficence interval.]  -> ConfidenceInterval

== ConfidenceLevel ==
What is Confidence Level?
* [It tells you how sure you can be] -> SamleExample
* [It represents how often the true percentage of the population  would pick the same answer as the sample did.] -> SamleExample

== ConfidenceInterval ==
What is Confidence Interval?
* [This is the margin of error.] -> SamleExample
* [It adds plus-or-minus figure to your answers.] -> SamleExample

== SamleExample ==
I see. So how big should be my sample if the population is 6000, the confidence level is 95% and the confidence interval 5?
* [36] -> Small
* [361] -> Plan
* [3610] -> Big

== Small ==
That small?! This can't be right.
* [Let's see then] -> SamleExample
* [If you are so smart why do you need my help?] -> END

== Big ==
That big?! That means I need to study almost half of the population. This doesn't sound right.
* [Let's see then.] -> SamleExample
* [If you are so smart why do you need my help?] -> END


== Plan ==
Alright. I believe Survey is my thing. What should I do next?
* [You need to describe your population] -> Population
* [Create a sample] -> Sample
* [Operationalise or quantify your research questions] -> Quantification
* [Create an instrument for data collection] -> Instruments
* [Collect data] -> CollectData
* [Polish data] -> PolishData
* [Analyse data] -> AnalyseData
* [Pilot] -> Pilot

== Instruments ==
What is the typical instrument for collecting survey data?
* Observation diary -> Diary
* Survey uses mostly Questionnaires -> Plan
* Interviews -> Interview

== Diary ==
This doesn't sound right. What will I observe?
* [Let's see then.] -> Instruments
* [If you are so smart why do you need my help?] -> END

== Interview ==
This doesn't sound right. How much data I can collect with the interview?
* [Let's see then.] -> Instruments
* [If you are so smart why do you need my help?] -> END

== CollectData ==
What do you mean by collecting data? I made a questionnaire already.
* [But now you need to send this questionnaire to your sample members.] -> RND2
* [Now you need to be sure that your sample is returning the answers.] -> Return

== RND2 ==
I'm not allowed to send the questionnaire to random people?
* [No. Then you can't generalise.] -> Generalisation
* [You can. And go home!] -> Fin

== Return ==
I need to make sure that they are returning the answers? How can I do that?
* [Send eminders.] -> Plan
* [Send questionnaie to bigger number of recipaints.] -> Plan

== PolishData ==
What do you mean I need to polish my data. Can I manipulate with numbers?
* [No, but you need to decide what to do with single missing answers?] -> Plan
* [You need to decide what to do with partly complete records?] -> Plan
* [Yes] -> Fin

== AnalyseData ==
* [You can use descriptive statistics methods] -> Descriptive
* [You can use statistical analysis] -> Statistics

== Descriptive ==
What are the descriptive statistics methods?
* [Counting] -> Plan
* [Averages] -> Plan
* [Freequency tables] -> Plan
* [Cross tables] -> Plan
* [Diagrams] -> Plan

== Statistics ==
What are the statistical analysis methods?
* [T-test] -> Difficult2
* [Anova] -> Difficult2
* [Correlation] -> Difficult2
* [Faxctor analysis] -> Difficult2
* [Dispersion analysis] -> Difficult2

== Difficult2 ==
No, this is too difficult. Are there any other methods?
* [Let's go back then.] -> AnalyseData
* [Go home then!] -> Fin

== Pilot ==
What do you mean by the pilot?
* [You need to test your data collection and analyse methods with a small group.] -> Test
* [You need to build a plane and be a pilot.] -> Fin

== Test ==
Why do I need to test my survey? Isn't this already enough to plan a survey and design a questionnaire? Now I need to do this twice!
* [Yes, but if you don't test your methods then you discover your mistakes when you start analysing the data.] -> Conclusion
* [Skiping the pilot is the typical mistake researchers do when they run a survey.] -> Conclusion

== Conclusion ==
~ wonDiscussion = true
I guess I need to accept that. Thank you for your help. -> END