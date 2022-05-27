using System;

namespace RBTree.Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree binaryTree = new();
            binaryTree.Put(10);
            binaryTree.Put(3);
            binaryTree.Put(27);
            binaryTree.Put(2);
            binaryTree.Put(5);
            binaryTree.Put(1);
            binaryTree.Put(4);
            binaryTree.Put(8);
            binaryTree.Put(6);
            binaryTree.Put(9);
            binaryTree.Put(11);
            binaryTree.Put(29);
            binaryTree.Put(12);
            binaryTree.Put(26);
            binaryTree.Put(30);

            binaryTree.Remove(10);

            Console.ReadKey();
   
        }
    }
}
