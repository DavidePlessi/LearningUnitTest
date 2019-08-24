# Characteristic of good unit tests
They are **first-class citizen** so they are as important, if not more than the production code and that means all the best practices we have learned about writing **clean, readable and maintainable** code apply to your test methods as well.  
Unit test should not have any logic.  
Test should be written and executed as if it's the only test in the world.  
Your tests should not be too specific or too general.

# What test and what not to test

## Do test
In program we have to types of functions: queries and commands.  
Query functions returns some value, so for testing they you should write a test and verify the returning value.  
Command functions often involves changing the state of an object in memory and/or writing to a database or calling a web service or sending a message to a message queue and so on. For this functions you should test the outcome of this method (if change the state of an object you should test the end state of the object).  

## Do not test
Language features, 3rd-party code

# Naming and organising tests
- For each project in your solution you'll have a unit testing project.  
  TestNinja -> TestNinja.UnitTests
- For each class in your project you'll have a test class.  
  Reservation -> ReservationTests
- The number of test are equal or greater than the number of execution paths, the name of your tests should clearly specify the business rule you're testing.  
    Naming convention: ```[MethodName]_[Scenario]_[ExpectedBehaviour]```

Now sometimes you are dealing with a large complex methos with so many execution paths and edge cases. In that case it may be better to dedicate a separate test class for that method. Because otherwise the test for this method may collude your test class.  
Example: CanBeCancelledBy can have a separate class following this naming convention ```[ClassName]_[MethodName]Tests``` -> Reservation_CanBeCancelledByTests

# Code example
See src\source-code-starter\TestNinja.UnitTests\MathTests.cs

# Writing Trustworthy tests
Test should be trustworthy.
- **What is a trustworthy test?** A trustworthy test is the kind of test we can rely on.
- **How can we write trustworthy test?** There are two way:
  - Use TDD
  - When you write your test after the production code, run your test if it passes, then go in the production code, and make a small change in the line that is supposed to make the test pass.