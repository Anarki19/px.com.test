namespace px.com.test;

class Program
{
    // я полностью отсепарировал два решения друг от друга.
    static void Main()
    {
        Console.WriteLine("Hello, Pasha!");
        
        var interviewSolution = new InterviewSolution();
        interviewSolution.Execute();
        
        var optimizedSolution = new OptimizedSolution();
        optimizedSolution.Execute();
        
        Console.ReadLine();
    }
}