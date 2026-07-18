using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities.
    // Expected Result: The item with the highest priority (B, priority 3) is dequeued first.
    // Defect(s) Found: Initially, the highest priority item was not removed from the queue.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 1);
        pq.Enqueue("B", 3);
        pq.Enqueue("C", 2);

        var result = pq.Dequeue();
        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority.
    // Expected Result: The first item enqueued with that priority (A) is dequeued first (FIFO).
    // Defect(s) Found: The loop skipped the last element in the queue before fix.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 2);
        pq.Enqueue("B", 2);
        pq.Enqueue("C", 2);

        var result = pq.Dequeue();
        Assert.AreEqual("A", result);
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: None after fix.
    public void TestPriorityQueue_EmptyQueue()
    {
        var pq = new PriorityQueue();
        var ex = Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Enqueue items, dequeue one, then check that the queue size decreases.
    // Expected Result: Queue count decreases by one after a dequeue.
    // Defect(s) Found: Before fix, items were not removed from the queue.
    public void TestPriorityQueue_DequeueRemovesItem()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("X", 5);
        pq.Enqueue("Y", 1);

        var result = pq.Dequeue();
        Assert.AreEqual("X", result);

        // Only one item should remain
        var nextResult = pq.Dequeue();
        Assert.AreEqual("Y", nextResult);
    }
}
