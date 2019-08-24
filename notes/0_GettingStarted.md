# Type of tests

## Unit
Tests a unit of an application without its external dependencies, they are cheap to write and execute fast, this will verify that each block in our application is working as expected.  
However since you're not testing these classes or components with their external dependencies you can't get a lot of confidence in the reliability of our application.

## Integration
Test the application with its external dependencies, so it the the integration of your application code with his concrete dependencies like files, databases. These test take longer to execute but it give more confidence.

## End-to-end
Drives an application through its UI. There are specific tools built for creating end-to-end tests. One popular tool is Selenium wich allows us to record the interaction of a user with our application and then play it back and check if the application is returning the right result or not.
This tests give you the greates confidence but they have 2 big problems:
- They are very slow
- They're very brittle, because a small enhancement to the application or a small chang in the user-interface can easily break these tests.

# Test pyramid
In an application we should integrate all kind of test following the test pyramid. This pyramid argue that most of your test should be in the category of unit tests because they are easy to write and they execute quickly.  
You should have a bunch of integration tests that test the integration of your software with external dependencies.  
And finally you should write very few end-to-end test for the key functions of the application, but you should not test edge cases.  
**Unit tests** are great for quickly testing the logic of conditional statements and loops.  
**Integration tests** are great for application that simply reads some data from or writes it to a database.  

# Tooling
There are several testing frameworks, the most populare are:
- NUnit
- MSTest (built into visual studio)
- XUnit
All of these give you a utility library to write your tests and a test runner which runs your test and gives you a report of passing and failing test.

# Code example
See src\source-code-starter\TestNinja
NB: To use NUnit we need to install the nu-get NUnit and NUnit3TestAdapter(needed to let VS recognize NUnit) version 3.8.1.  

MSTest example:  
```c#
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]//This attributes belong to MSTestFramework
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange (where we initialize our objects)
            var reservation = new Reservation();

            // Act (where we act on this object)
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });


            // Assert (where we verify the result)
            Assert.IsTrue(result);
        }
    }
```

NUnit example:  
```c#
    [TestFixture]
    public class ReservationTests
    {
        [Test]//This attributes belong to MSTestFramework
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange (where we initialize our objects)
            var reservation = new Reservation();

            // Act (where we act on this object)
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });


            // Assert (where we verify the result)
            Assert.IsTrue(result);
            //or
            Assert.That(result, Is.True);
            //or
            Assert.That(result == true);
        }
    }
```

# Test driven development
With TDD you write your tests before writing the production code.  
How it works:
- Write a failing test
- Write the simplest code to make the test pass
- Refactor if necessary  
Benefit:
- Testable source code
- Full coverage by tests
- Simpler implementatio