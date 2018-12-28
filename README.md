# BackEndCaseStudy
Case Study for Deloitte - See README.TXT for more detailed information

Application developed using Visual Studio 2017 and C#

Application "Main" entry point resides in the project deloitte.ActivitiesGenerator.Cmd. This generates 
a console application. It contains little business logic except extraction of command line arguments
and error output. However, both these items should be located elsewhere in interface based classes, 
so that other argument & error messaging mechanisms are accomodated.

Business logic resides in deloitte.ActivitiesGenerator.Cmd. It is independent of the calling interface, 
so that any interface may utilise it, for example a Desktop/Web Service, Desktop/Web GUI.

Unit tests are located in deloitte.ActivitiesGenerator.Test. 
