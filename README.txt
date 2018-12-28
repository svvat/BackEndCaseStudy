Abbreviations
=============

TD - Technical Debt
SOLID - SOLID Principles (Single, Open/closed, Liskov, Interface, Dependency)

A Note regarding the Specification.
=========================================
The example output is incorrect. The specification states that Lunch starts at 12 noon.

A Note regarding the application's output
=========================================
The application takes activities in order as provided. However, this is not the most efficient for 
filling the day contiguously. For example Team 1 has "Learning Magic Tricks" starting at 10:45 AM 
with a duration of 40min. This leaves 35 minutes till lunch. As the next item's duration was longer 
than 35minutes, it is scheduled after lunch. It would be "more correct" to identify suitable tasks 
by duration to fill time more efficiently. However this was not part of the spec and would be 
considered "spec creep" and would be recomended for a later version. This would also require code 
to ensure activites do not overlap with other teams, ie at any one time only one team would be 
allocated to one activity.

A generated output is provided in the deloitte.ActivitiesGenerator.Cmd project named "GeneratedOutput28Dec2018.txt"

Solution Structure
==================

Application "Main" entry point resides in the project deloitte.ActivitiesGenerator.Cmd. This generates 
a console application. It contains little business logic except extraction of command line arguments
and error output. However, both these items should be located elsewhere in interface based classes, 
so that other argument & error messaging mechanisms are accomodated.

Business logic resides in deloitte.ActivitiesGenerator.Cmd. It is independent of the calling interface, 
so that any interface may utilise it, for example a Desktop/Web Service, Desktop/Web GUI.

Unit tests are located in deloitte.ActivitiesGenerator.Test. 

Application Execution
=====================
The application executable is delivered in the deloitte\bin\Debug directory, named "ActivitiesGenerator.exe".

Using windows explorer, drop the file "activities.txt" onto the executable named "ActivitiesGenerator.exe". 
The output file named "schedule.txt" will be created in the same folder.

If an exception occurs, the application writes to the StdOut rather than StdError (TD). To view exception 
error messages, the application may be run from a cmd shell session.

The application may be run from Visual Studio. If no command line arguments are specified, it will open 
the file "activities.txt" in the current (execution) folder. A sample file is included with the solition.

Development notes
=================

The application is developed using Visual Studio 2017.

The application is developed as a rapid prototype, with TD, and would benefit from refactoring.

The project solution is loaded into Visual Studio with the project solution named deloitte.ActivitiesGenerator.sln.

Visual Studio & C# were used rather than Java due to reasons of concurrent familiarity. However, the code 
was written with Java style and used get & set methods rather than C# properties.

The application "should" satisfy the specification - barring any unintentional spec misinterpretation. 
TD exists to satisfy "product delivery" by the deadline of the 28th December.

All public methods are protected by exception handling. 

Public methods are "thin" and protected by try/catch, except for accessors. In more secure / robust applications, 
all methods are wrapped in try/catch blocks.

All Exceptions are based on an application specific exception.

In production, Exceptions should not display messages that expose internals of the code. Messages should display 
a code number which may be cross referenced with code.

Constructors should not contain assignments and "work" code and should not cause exceptions.

Singletons are used rather than statics to facilitate unit test mocking.

All classes are based on interfaces.

Derived data is not stored. Any performance impact is not significant for the application.

Code uses interfaces rather than classes (SOLID), to simplify unit tests and facilitate dependency injection.

If private method warrant unit tests due to their complexity, the implication is that they should be drawn 
out in to their own separate interface based class.

Class instantiations are located separately from the usage.

Constants are used rather than "Magic Numbers" to facilitate comprehension and dynamic configurability.

Classes should follow SOLID. To meet the deadline, some classes are not SOLID and would remain as TD for 
refactoring. Completion of unit tests very quickly identifies classes that need refactoring.

The code has been delivered to specifically write to a text file. Another class/interface abstration layer 
should be implemented to remove references to the StreamReader/Writer. This would removes dependency on 
text files and simplify unit tests. In this project, the class Schedule is a good candidate for such attention.

Unit Tests
==========
All public methods should be unit tested. (TD)

All exception handling should be tested. (TD)

Unit Tests are incomplete. Technical debt is acceptable in order to satisfy requirements and meet the deadline.

MSTest was used to expedite development. With more concurrent familiarity, NUnit would be used for better 
mocking & test coverage tools.


