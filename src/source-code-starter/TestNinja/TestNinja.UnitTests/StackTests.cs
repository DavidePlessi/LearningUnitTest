using System;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Count_EmptyList_ReturnZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void Count_WhenCalled_ReturnTheNumberOfElement()
        {
            _stack.Push("a");
            _stack.Push("b");
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Push_NullObject_ThrowArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidObject_AddObjectToStack()
        {
            var count = _stack.Count;
            _stack.Push("Hi");
            Assert.That(_stack.Count, Is.EqualTo(count + 1));
        }

        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Pop_FullStack_ReturnLastObject()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            
            var res = _stack.Pop();
            
            Assert.That(res, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_FullStack_RemoveLastObject()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            
            _stack.Pop();
            
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Peek_FullStack_ReturnLastObject()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            
            var res = _stack.Peek();
            
            Assert.That(res, Is.EqualTo("c"));
        }
        
        [Test]
        public void Peek_FullStack_DoNotRemoveLastObject()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            
            _stack.Peek();
            
            Assert.That(_stack.Count, Is.EqualTo(3));
        }

    }
}