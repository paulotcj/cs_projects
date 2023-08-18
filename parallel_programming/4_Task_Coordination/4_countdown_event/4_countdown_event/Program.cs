

int task_count = 5;
var cte = new CountdownEvent(task_count);
var random = new Random();


//tasks will be started here, but they are not 'waited' we just signal when the task reaches a point
for (int i = 0; i < task_count; i++) {
    Task.Factory.StartNew(() => {
        Console.WriteLine($"Entering task: {Task.CurrentId}");
        Thread.Sleep( random.Next(1000) );
        Console.WriteLine($"Exiting task: {Task.CurrentId}");
        cte.Signal();
    });
}


//we start a new task here, which should wait all tasks from the countdown to finish
var final_task = Task.Factory.StartNew(() => {
    Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
    cte.Wait();
    Console.WriteLine($"All tasks from the loop signaled they are done. From: {Task.CurrentId}");
});
final_task.Wait();

Console.WriteLine("All Done");
