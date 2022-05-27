using System;

namespace RBTree.Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            RBTree дерево = new();
            дерево.Put(10, 20);
            дерево.Put(11, 20);
            дерево.Put(9, 20);
            дерево.Put(7, 10);


            var a = дерево.root;
            Console.WriteLine(a.key);
            Console.WriteLine(a.left.key);
            Console.WriteLine(a.right.key);

            дерево.Remove(10);

            Console.WriteLine(a.key);
            Console.WriteLine(a.left.key);
   
        }
    }
}
