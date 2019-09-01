# Introduction
Imagin you have a class called video service, this class uses the file class to read the content of a file. In your application this video service may use the db context class to read a record from the database so it has a dependency to an external resourc. In order to unit test the video service class, you should do some surgery in your code and **decouple** the video service from the file class or db context or whatever that external dependency is this way when unit testing this class you can replace the file class with another class that looks like the file class from the outside but does't talk to the file system. In fact, it doesn't do anything, its like a class with bunch of methods that have no implementation. We call this class a fake, or a test double.  
**When unit testing classes with external dependencies, we replace a production object with a test double**

# Loosely-coupled and testable code
In a loosely coupled design you can replace on ogject with another at run time. so when unit testing a class that uses an object that taks to an external resource, you can replace that object with a fake object, which we call a test double. Now there are three teps that you need to followç
- You extract the code that uses an external resource into a separate class. 
- You put the code that talks to an external resource into a separate class and isolate it from the rest of your code.
- You extract an interface from that class. You can have two different classes that implement that interface. One is the real implementation that uses an external resource, the other is the fake one which we call the test double. So instead of being dependent of a specific implementation it will be dependent only on the interface, you can pass any object that implements that interface.  
And this way the class becomes loosely coupled and testable.
Example:
Before:  
```c#
public void MyMethod()
{
    var reader = new FileReader();
    reader.Read()
}
```  
After:  
```c#
public void MyMethod(IFileReader reader)
{
    reader.Read()
}
```  
This is dependecy injection: it simply means to inject or supply the dependencies of a class from the outside, and this makes your classes loosely coupled and testable.

# Refactoring towards a Loosely-coupled design
See src\source-code-starter\TestNinja\VideoService.cs

# Dependency injection via method parameters
See src\source-code-starter\TestNinja\VideoService.cs

# Dependency injection via properties
See src\source-code-starter\TestNinja\VideoService.cs

# Dependency injection via constructor
See src\source-code-starter\TestNinja\VideoService.cs

# Dependency injection framework
The previous method of injection work but it's not ideal in enterprise applications. Because in a real world application this class might have a couple more dependencies.  
It is called Poor man's dependency injection.  
Enterprise application usually use an injection framework.  
In a dependency injection framework, you have a container, this container is a registry of all your interfaces and their implementations. When your application starts, your dependency injection framework will automatically take care of creating object graphs based on the interfaces and types registered in the container.  
When you initialize an object the dependency injection framework kicks in, it looks at the parameters of the constructor then it looks at this container or registry and finds the concrete implementations for these referenced interfaces, instantiates them, and passes them to your object.

# Mocking Frameworks
Help to create fake object (as our FakeFileReader) dynamically. We are goingi to use **Moq**.

# Creating Mock objects using Moq
We need to add the package to TestNinja.UnitTest project.  
See src\source-code-starter\TestNinja.UnitTest\VideoServiceTests.cs

# State-based vs Interaction Testing
In the unit test we have written so far, we asserted that our methods returned the right value or set the right state, this is calle state-based testing.  
However, sometimes we are dealing with code that touches external resources, we need to verify the class we're testing interact with another class properly, this is what we call Interaction testing.  
**Use interaction tests only when dealing with external resources.**  

# Testing the interaction between two objects
See src\source-code-starter\TestNinja\OrderService.cs
