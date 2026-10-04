using wt_lab3_3mvc_ageyavich.Controllers;

namespace wt_lab3_3mvc_ageyavich;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var controller = new ProductController();
        controller.Run();
    }
}
