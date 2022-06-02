using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTree.Tree
{
    class RBTree
    {
        private static bool BLACK = false;
        private static bool RED = true;
        private int lastRemoved = -1;
        public Node root = null;
        private int size = 0;

        public int Get (int key)
        {
            return FindValue(key);
        }

        
        public void Put(int key,int value)
        {
            size++;
            root = PutElement(root, key, value);
        }

    public int Remove(int key)
        {
            root = DeleteElement(root, key);
            size = lastRemoved == -1 ? size : --size;
            return lastRemoved;
        }

    public int Min()
        {
            if (root == null)
            {
                return default;
            }
            return FindMin(root).key;
        }

    public int MinValue()
        {
            if (root == null)
            {
                return default;
            }
            return FindMin(root).value;
        }

    public int Max()
        {
            if (root == null)
            {
                return default;
            }
            return FindMax().key;
        }


    public int MaxValue()
        {
            if (root == null)
            {
                return default;
            }
            return FindMax().value;

        }

    public int Floor( int key)
        {
            Node result = FindFloor(root, key);
            return result == null ? -1: result.key;
        }

    public int Ceil( int key)
        {
            Node result = FindCeil(root, key);
            return result == null ? -1 : result.key;
        }

        
    public int Count()
        {
            return size;
        }

        private Node FindMin(Node from)
        {
            Node current = from;
            while (current.left != null)
            {
                current = current.left;
            }
            return current;
        }

        private Node FindMax()
        {
            Node current = root;
            while (current.right != null)
            {
                current = current.right;
            }
            return current;
        }

        private Node FindFloor(Node start, int key)
        {
            if (start == null)
            {
                return null;
            }

            int compare = key.CompareTo(start.key);
            if (compare == 0)
            {
                return start;
            }
            if (compare < 0)
            {
                return FindFloor(start.left, key);
            }

            Node part = FindFloor(start.right, key);
            return part == null ? start : part;
        }

        private Node FindCeil(Node start, int key)
        {
            if (start == null)
            {
                return null;
            }

            int compare = key.CompareTo(start.key);
            if (compare == 0)
            {
                return start;
            }

            if (compare > 0)
            {
                return FindCeil(start.right, key);
            }

            Node part = FindCeil(start.left, key);
            return part == null ? start : part;
        }

        private Node PutElement(Node node, int key, int value)
        {
            if (node == null)
            {
                return new Node(key, value, RED);
            }

            int compare = node.key.CompareTo(key);
            if (compare > 0)
            {
                node.left = PutElement(node.left, key, value);
            }
            else if (compare < 0)
            {
                node.right = PutElement(node.right, key, value);
            }
            else
            {
                size--;
                node.value = value;
            }
            return FixUp(node);
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.right) && !IsRed(node.left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.left) && IsRed(node.left.left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.left) && IsRed(node.right))
            {
                FlipColors(node);
            }
            return node;
        }

        private bool IsRed(Node node)
        {
            return node != null && node.color == RED;
        }

        private void FlipColors(Node node)
        {
            node.color = !node.color;
            node.left.color = !node.left.color;
            node.right.color = !node.right.color;
        }

        private Node RotateLeft(Node node)
        {
            Node current = node.right;
            node.right = current.left;
            current.left = node;
            current.color = node.color;
            node.color = RED;
            return current;
        }

        private Node RotateRight(Node node)
        {
            Node current = node.left;
            node.left = current.right;
            current.right = node;
            current.color = node.color;
            node.color = RED;
            return current;
        }

        private int FindValue(int key)
        {
            Node current = root;
            int compare;
            while (current != null)
            {
                compare = current.key.CompareTo(key);
                if (compare > 0)
                {
                    current = current.left;
                }
                else if (compare < 0)
                {
                    current = current.right;
                }
                else
                {
                    return current.value;
                }
            }
            return -1;
        }

        private Node DeleteElement(Node node, int key)
        {
            if (node == null)
            {
                lastRemoved = -1;
                return null;
            }

            int current = key.CompareTo(node.key);
            if (current < 0)
            {
                if (node.left != null)
                {
                    if (!IsRed(node.left) && !IsRed(node.left.left))
                    {
                        node = MoveRedLeft(node);
                    }
                }
                node.left = DeleteElement(node.left, key);
                return FixUp(node);
            }
            else
            {
                if (IsRed(node.left))
                {
                    node = RotateRight(node);
                    node.right = DeleteElement(node.right, key);
                    return FixUp(node);
                }

                if (node.key == key && node.right == null)
                {
                    lastRemoved = node.value;
                    return null;
                }

                if (node.right != null && !IsRed(node.right) && !IsRed(node.right.left))
                {
                    node = MoveRedRight(node);
                }

                if (node.key == key)
                {
                    lastRemoved = node.value;
                    Node minNode = FindMin(node.right);
                    node.key = minNode.key;
                    node.value = minNode.value;
                    node.right = DeleteMin(node.right);
                }
                else
                {
                    node.right = DeleteElement(node.right, key);
                }
            }
            return FixUp(node);
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);
            if (IsRed(node.right.left))
            {
                node.right = RotateRight(node.right);
                node = RotateLeft(node);
                FlipColors(node);
            }
            return node;
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);
            if (IsRed(node.left.left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }
            return node;
        }

        private Node DeleteMin(Node node)
        {
            if (node.left == null)
            {
                return null;
            }

            if (!IsRed(node.left) && !IsRed(node.left.left))
            {
                node = MoveRedLeft(node);
            }

            node.left = DeleteMin(node.left);
            return FixUp(node);
        }

        public void ConsoleWrite()
        {
            TreeOne(root, 0, true);
        }

        private void TreeOne(Node cur, int lvl, bool thisstring)
        {
            if (cur == null) // end of the branch 
            {
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("->");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("null");
                return;
            }
            if (cur != root)
            {
                if (!thisstring) // move to the next string 
                {
                    int tempcount = lvl;
                    while (tempcount > 0)
                    {
                        Console.Write("\t");
                        tempcount--;
                    }

                }
                else // keep writing in this string 
                    Console.Write("\t");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("->");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(cur.key + "/" + cur.value);

            if (cur == root) // with left branch always keep in this string 
            {
                TreeOne(cur.left, lvl + 1, true);
            }
            else
                TreeOne(cur.left, lvl + 1, true);

            if (cur.right != null)
            {
                Console.WriteLine();
                TreeOne(cur.right, lvl + 1, false); //with right branch always move to the next string
            }
        }

        public class Node
        {
            public int key;
            public int value;
            public Node left;
            public Node right;
            public bool color;

            public Node(int key, int value, bool color)
            {
                this.key = key;
                this.value = value;
                this.color = color;
            }
        }
    }
}
