/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Inputs because I got lazy and tired of typing them in over and over.
        string input = string.Join(Environment.NewLine,
            "Mario", "913", "Ran out of mushrooms",
            "Luigi", "714", "House is haunted",
            "Wario", "1021", "Shouldn't be here?"
        );

        Console.SetIn(new StringReader(input));

        // Test Cases

        // Test 1
        // Scenario: Creates Queue then stores the max size.
        // Expected Result: Prints max queue size, in this case 2.
        Console.WriteLine("Test 1: Takes Max Queue Size");
        var mainTest = new CustomerService(2);
        Console.WriteLine($"\nQueue Max Size: {mainTest._maxSize}");
        Console.WriteLine("\nHope the above line said '2', otherwise you bungled it.");

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 1.5
        // Scenario: Sets max queue size to 10 if the number given is invalid.
        // Expected Result: Prints max queue size, in this case 10.
        Console.WriteLine("Test 1.5: Defaults Max Queue on invalid value");
        var badQueue = new CustomerService(-3);
        Console.WriteLine($"\nQueue Max Size: {badQueue._maxSize}");
        Console.WriteLine("\nHope the above line said '10', otherwise you bungled it.");

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Successfully adds 2 customers to the queue. 
        // Expected Result: Prints customer's info, and size of queue which should be 2.
        mainTest.AddNewCustomer();        
        mainTest.AddNewCustomer();       
        Console.WriteLine($"\nQueue Size: {mainTest._queue.Count()}");
        foreach (Customer item in mainTest._queue)
        {
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine("\nHope the above printed a size of 2 and all the customer info, otherwise you bungled it.");

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Throws error if you try to add a customer when the queue is full.
        // Expected Result: Throws error.
        Console.WriteLine("Test 3: Enforcing Queue Limits");
        mainTest.AddNewCustomer();
        
        Console.WriteLine("\nHope you got that error message, otherwise you bungled it.");

        // Defect(s) Found: Added 3rd customer to queue. FIXED: Changed > to >=

        Console.WriteLine("=================");
        
        // Test 4
        // Scenario: Successfully dequeues a customer, displaying their info.
        // Expected Result: Dequeues a customer, printing their info, before printing the new queue size (which should be 1).
        Console.WriteLine("Test 4: 'ServeCustomer' Does Its Job");
        mainTest.ServeCustomer();
        Console.WriteLine($"\nQueue Size: {mainTest._queue.Count()}");

        Console.WriteLine("\nHope it printed the serviced customer info and then a size of 1, otherwise you bungled it.");

        // Defect(s) Found: Printed info for customer after serviced one. FIXED: Swapped teh spots of grabbing the customers info and removing them from the queue.

        Console.WriteLine("=================");
        
        // Test 5
        // Scenario: Services remaining customer, then prints an error when it tries to service again.
        // Expected Result: Dequeues a customer, printing their info, before throwing an error.
        Console.WriteLine("Test 5: Can't Serve What Ain't There");
        mainTest.ServeCustomer();
        mainTest.ServeCustomer();

        Console.WriteLine("\nHope it printed the serviced customer info and then an error, otherwise you bungled it.");
        
        // Defect(s) Found: Caused an exception instead of in-program error message.

        Console.WriteLine("=================");
        
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count() == 0)
        {
            Console.WriteLine("No Customers in Queue");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}