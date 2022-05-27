using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTree.Tree
{
    public class BinaryTree
    {
        public Node root;

        public class Node
        {
            public int value;
            public Node left = null;
            public Node right = null;

            public Node(int value)
            {
                this.value = value;
            }
        }

        public void Put(int value)
        {
            Node newNode = new(value);
            if (root == null)
            {
                root = newNode;
                return;
            }

            Node current = root;
            Node prev = null;
            while (current != null)
            {
                if (current.value > value)
                {
                    prev = current;
                    current = current.left;
                    continue;
                }

                prev = current;
                current = current.right;
            }

            if (prev.value > value)
            {
                prev.left = newNode;
            } else
            {
                prev.right = newNode;
            }
        }

        public bool Search(int value)
        {
            Node current = root;
            while (current != null)
            {
                if (value == current.value)
                {
                    return true;
                }

                if (current.value > value)
                {
                    current = current.left;
                    continue;
                }

                current = current.right;
            }
            return false;
        }

        public void Remove(int value)
        {
            if (root == null)
            {
                Console.WriteLine("НУ НЕТЬ ТУТ НИЧЕВО!!!");
                return;     
            }


            Node current = root;
            Node prev = null;
            while (current != null)
            {
                if (value == current.value)
                {
                    if (prev == null)// need to remove the root of the tree
                    {
                        if (root.left != null)
                        {
                            var right = root.right;
                            var rightofleft = root.left.right;
                            root = root.left;
                            root.right = right;
                            root.left.right = rightofleft;
                        }
                        else //the branch on the left is missing
                        {
                            root = root.right;
                        }
                        return;
                    }

                    if (value > prev.value)
                    {
                        if (current.left != null)
                        {
                            Node temp = current.left; // find the last right value for the branch on the left 
                            Node prevTemp = null;
                            //current = current.left;

                            while (temp.right != null)
                            {
                                prevTemp = temp;
                                temp = temp.right;
                            }

                            if (prevTemp != null)
                            {
                                prevTemp.right = null;
                                prev.right = temp;
                                temp.left = current.left;
                                temp.right = current.right;
                            }
                            else
                            {
                                prev.right = current.left;
                                prev.right.right = current.right;
                            }
                        }
                        else
                        {
                            prev.right = current.right;
                        }
                        //if (current.left != null)
                        //{
                        //    current.left.right = current.right;
                        //    prev.right = current.left;
                        //} else
                        //{
                        //    prev.right = current.right;
                        //}
                    } 
                    else
                    {
                        if (current.right != null)
                        {
                            Node temp = current.right;
                            Node prevTemp = null;

                            while ( temp.left != null)
                            {
                                prevTemp = temp;
                                temp = temp.left;
                            }

                            if (prevTemp != null)
                            {
                                prevTemp.left = null;
                                prev.left = temp;
                                temp.right = current.right;
                                temp.left = current.left;
                            }
                            else
                            {
                                prev.left = current.right;
                                prev.left.left = current.left;
                            }

                        }
                        else
                        {
                            prev.left = current.left;
                        }
                        //if (current.right != null)
                        //{
                        //    current.right.left = current.left;
                        //    prev.left = current.right;
                        //}
                        //else
                        //{
                        //    prev.left = current.left;
                        //}
                    }

                }

                if (current.value > value)
                {
                    prev = current;
                    current = current.left;
                    continue;
                }

                prev = current;
                current = current.right;
            }
        }

        public void ConsoleWrite()
        {
            Tree(root);
        }

        public void Tree(Node current)
        {
            //if (current == null)
            //    return;
            //int tempcount = level;
            //while (tempcount > 0)
            //{
            //    Console.Write("\t");
            //    tempcount--;
            //}
            //Console.Write("->");
            //Console.Write(current.value);
            //if (current == root)
            //{
            //    Tree(current.left, level + 1);
            //}
            //else
            //    Tree(current.left, level);
            //Console.WriteLine();
            //Tree(current.right, level+1);

            if (current == null)
                return;
            Console.WriteLine(current.value);
            Tree(current.left);

            Tree(current.right);

        }
    }
}
