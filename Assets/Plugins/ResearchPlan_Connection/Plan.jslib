mergeInto(LibraryManager.library, {

  SavePlanTitle: function (str) {
    window.localStorage.setItem('plan-title', Pointer_stringify(str));
  },
  SavePlanAuthor: function (str) {
    window.localStorage.setItem('plan-author', Pointer_stringify(str));
  },
  SavePlanAuthorEmail: function (str) {
    window.localStorage.setItem('plan-author-email', Pointer_stringify(str));
  },
  SavePlanSupervisor: function (str) {
    window.localStorage.setItem('plan-supervisor', Pointer_stringify(str));
  },
  SavePlanSupervisorEmail: function (str) {
    window.localStorage.setItem('plan-supervisor-email', Pointer_stringify(str));
  },
  // (General) Problem or Needs
  SavePlanProblem: function (str) {
    window.localStorage.setItem('plan-problem', Pointer_stringify(str));
  },
  // Research Objective(s)
  SavePlanObjectives: function (str) {
    window.localStorage.setItem('plan-objective', Pointer_stringify(str));
  },
  // Research Questions
  SavePlanQuestions: function (str) {
    window.localStorage.setItem('plan-question', Pointer_stringify(str));
  },
  // Main references
  SavePlanReferences: function (str) {
    window.localStorage.setItem('plan-reference', Pointer_stringify(str));
  },
  // Research design (methodology)
  SavePlanMethodology: function (str) {
    /*
    Accepted values (single value):
    MetaStudy
    Experiment
    Survey
    CaseStudy
    NarrativeStudy
    EthnographicStudy
    PhenomenologicalStudy
    DiscursiveStudy
    HermeneuticStudy
    HistoricalStudy
    GroundedTheory
    ActionResearch
    ParticipatoryResearch
    DesignBasedResearch
    ResearchByDesign
    DesignScienceResearch
    DesignThinking
    EvaluationStudy
    */
    window.localStorage.setItem('plan-methodology', Pointer_stringify(str));
  },
  // Data collection, development or analyse methods
  SavePlanMethods: function (str) {
    // Save in JSON, then convert it to string, before calling this function
    // Sample: '["Test", "Questionnaire", "StructuredInterview"]'
    /*
    Accepted values (multiple values):
    Test
    Questionnaire
    StructuredInterview
    SemiStructuredInterview
    UnstructuredInterview
    FocusGroupInterview
    DocumentStudy
    Observation
    ParticipantObservation
    StimulatedRecall
    BiometricResearch
    Netnography
    Journaling
    ImplicitMethods
    RecordsOfBehaviour
    Logfiles
    Stories
    Ideation 
    Persona
    Storyboarding
    ParticipatoryDesign
    CardSorting
    Prototyping
    Walkthrough
    */
    window.localStorage.setItem('plan-method', Pointer_stringify(str));
  },
});