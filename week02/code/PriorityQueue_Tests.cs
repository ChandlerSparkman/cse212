using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Adds items to the queue, the dequeues one (covering requirements 1 and 2).
    // Expected Result: Lists the items in the queue, Copper, Iron, Gold, Silver, Diamond. Dequeues Diamond, then checks the queue again.
    // Defect(s) Found: Doesn't seem to check final item in queue, nor removes it. Changed starting i to 0 from 1, and deleted the -1 from _queue.Count. Also added a RemoveAt to the dequeue function.
    public void TestPriorityQueue_Queue_and_Dequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Copper", 0);
        priorityQueue.Enqueue("Iron", 1);
        priorityQueue.Enqueue("Gold", 3);
        priorityQueue.Enqueue("Silver", 2);
        priorityQueue.Enqueue("Diamond", 4);

        string actualQueue = priorityQueue.ToString();
        string expectedQueue = "[Copper (Pri:0), Iron (Pri:1), Gold (Pri:3), Silver (Pri:2), Diamond (Pri:4)]";

        Assert.AreEqual(actualQueue, expectedQueue);

        string dequeuedItem = priorityQueue.Dequeue();

        actualQueue = priorityQueue.ToString();
        expectedQueue = "[Copper (Pri:0), Iron (Pri:1), Gold (Pri:3), Silver (Pri:2)]";

        Assert.AreEqual(actualQueue, expectedQueue);
    }

    [TestMethod]
    // Scenario: Dequeues an item with a shared priority (covering requirement 3).
    // Expected Result: Should return Platinum, not Diamond.
    // Defect(s) Found: Returned Diamond. Changed >= to just >.
    public void TestPriorityQueue_Dequeue_Same_Prio()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Copper", 0);
        priorityQueue.Enqueue("Iron", 1);
        priorityQueue.Enqueue("Platinum", 4);
        priorityQueue.Enqueue("Silver", 2);
        priorityQueue.Enqueue("Diamond", 4);

        string dequeuedItem = priorityQueue.Dequeue();

        Assert.AreEqual(dequeuedItem, "Platinum");
    }

    [TestMethod]
    // Scenario: Dequeues an empty queue.
    // Expected Result: Throws an error message saying "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_ErrorHandling()
    {
        var priorityQueue = new PriorityQueue();
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}