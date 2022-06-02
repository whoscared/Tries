using System;

namespace RBTree.Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();
            tree.Put(3);
            tree.Put(4);
            tree.Put(8);
            tree.Put(1);
            tree.Put(333);
            tree.Put(30);
            tree.Put(36);
            tree.Put(13);
            tree.ConsoleWrite();
            Console.WriteLine();

            RBTree redbl = new();
            redbl.Put(0,3);
            redbl.Put(1,4);
            redbl.Put(2,8);
            redbl.Put(3,1);
            redbl.Put(4,333);
            redbl.Put(5,30);
            redbl.Put(6,36);
            redbl.Put(7,13);

            redbl.ConsoleWrite();
   
        }
    }
}
