using System;

namespace LinkedList
{
    public class Book
    {
        public string name;
        public string author;
        public int pages;
        public Book(string name, string author, int pages)
        {
            this.name = name;
            this.author = author;
            this.pages = pages;
        }
        public Book() //empty constructor to be able to save elementAt() data in an empty object
        {

        }
        public static void RegisterData(MyList list)
        {
            Console.WriteLine("How many books?");
            int bookCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < bookCount; i++)
            {
                Console.WriteLine("Enter book name: ");
                string bookName = Console.ReadLine();
                Console.WriteLine("Enter book author: ");
                string bookAuthor = Console.ReadLine();
                Console.WriteLine("Enter book pages: ");
                int pages = Convert.ToInt32(Console.ReadLine());
                list.AddEnd(new Book(bookName, bookAuthor, pages));
            }
            Console.WriteLine("Books registered!\n");
        }
    }
    public class Node //list element class. Link with Book objects
    {
        public Node nextNode;
        public Book book; //stores necessary data type for node
        public Node(Book book)
        {
            this.book = book;
        }
    }
    public class MyList
    {
        public Node firstNode;
        public void AddEnd(Book book) //adding at end of list
        {
            Node node = new Node(book); //item to add
            if (firstNode == null) //when first item gets added
            {
                firstNode = node;
                return;
            }
            Node current = firstNode; //sets first item as current
            while (current.nextNode != null) //moves up the list and sets current as the next one, till last one is null
            {
                current = current.nextNode;
            }
            //when next is null, add to end
            current.nextNode = node;
        }
        public void PrintList()
        {
            Node current = firstNode; //sets current as first item
            while (current != null)
            {
                Console.WriteLine("Name: {0}, Author: {1}, Pages: {2}", current.book.name, current.book.author, current.book.pages);
                current = current.nextNode; //sets current as next after every iteration
            }
        }
        public int GetCount()
        {
            int count = 0; //define counter
            Node current = firstNode;
            while (current != null)
            {
                current = current.nextNode;
                count++;
            }
            return count;
        }
        public void RemoveAt(int index)
        {
            //for 0 element
            if (index == 0)
            {
                firstNode = firstNode.nextNode; //1st will be next, deleting 0 element
                return;
            }
            //for middle elements
            int counter = 0;
            Node current = firstNode;
            while (current != null)
            {
                if (counter == index - 1) //element before index, to move the chain to the element after index (deleting the one in middle, which is index)
                {
                    break; //stop loop
                }
                current = current.nextNode;
                counter++;
            }
            if (current != null && current.nextNode != null) //checking if current is not empty or next is not empty (out of bounds)
            {
                current.nextNode = current.nextNode.nextNode; 
            }
        }
        public void RemoveItem(Book book)
        {
            //for 0 item
            if (firstNode.book == book) //if first.book matches passed book
            {
                firstNode = firstNode.nextNode; //first is now next after original first
                return;
            }
            //for other item
            Node current = firstNode; //sets current as first item
            Node previous = firstNode;
            while (current != null)
            {
                if (current.book == book) //checking through next
                {
                    break;
                }
                previous = current; 
                current = current.nextNode; //sets current as next after every iteration
            }
            previous.nextNode = current.nextNode; //previous next link will be where current next link was supposed to be, removing current between.
        }
        public int FirstIndexOf(Book book)
        {
            int counter = 0; //define counter
            Node current = firstNode; //sets current as first item
            while (current != null)
            {
                if (current.book == book) //checking through next
                {
                    break; //break out of loop
                }
                current = current.nextNode;
                counter++;
            }
            return counter;
        }
        public void ReverseList()
        {
            Node previous = null, current = firstNode, next = null;
            while (current != null)
            {
                next = current.nextNode;
                current.nextNode = previous;
                previous = current;
                current = next;
            }
            firstNode = previous;
        }
        public int LastIndexOf(Book book)
        {
            ReverseList();
            int counter = 0;
            int reversed_counter = 0;
            Node current = firstNode;
            while (current != null)
            {
                counter++;
                current = current.nextNode;
            }

            counter = counter - 1;
            current = firstNode;

            while (current != null)
            {
                if (current.book == book)
                {
                    break;
                }
                reversed_counter++;
                current = current.nextNode;

            }
            int correct_index = reversed_counter - counter;
            ReverseList();
            return System.Math.Abs(correct_index); //modulis
        }
        public void InsertItem(Book book, int index)
        {
            Node node = new Node(book); //item to insert
            //if index is 0
            if (index == 0)
            {
                node.nextNode = firstNode; //adjusting pointers so no data is lost
                firstNode = node;
                return; //stops code here
            }
            int counter = 0; //define counter
            Node current = firstNode;
            while (current != null)
            {
                if (counter == index - 1) //index found (previous before set index)
                {
                    break;
                }
                current = current.nextNode;
                counter++;
            }
            node.nextNode = current.nextNode; //added nodes (next) is the node that was in prefered index
            current.nextNode = node; //previous is now added node
        }
        public void Clear()
        {
            firstNode = null; //no firstnode - list does not exist anymore
        }
        public Book ElementAt(int index)
        {
            int counter = 0;
            Node current = firstNode;
            while (current != null)
            {
                if (counter == index)
                {
                    break; //stop loop
                }
                current = current.nextNode;
                counter++;
            }
            //for existing and non existing values 
            if (current != null)
            {
                return current.book;
            }
            else
            {
                return null;
            }
        }
    }
    class Program
    {
        static void InsertBook(MyList list)
        {
            Console.WriteLine("Enter position index: ");
            int index = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entering data....");
            Console.WriteLine("Enter book name:");
            string bookName = Console.ReadLine();
            Console.WriteLine("Enter book author:");
            string bookAuthor = Console.ReadLine();
            Console.WriteLine("Enter book pages: ");
            int pages = Convert.ToInt32(Console.ReadLine());
            try
            {
                list.InsertItem(new Book(bookName, bookAuthor, pages), index);
            }
            catch (Exception)
            {

                Console.WriteLine("Insertion unsuccesfull! Try a valid index.");
            }
        }
        static void RemoveByIndex(MyList list)
        {
            Console.WriteLine("Enter index you would like to remove: ");
            int index = Convert.ToInt32(Console.ReadLine());
            try
            {
                list.RemoveAt(index);
            }
            catch (Exception)
            {
                Console.WriteLine("Removal unsuccesfull. Check index.");
            }
        }
        static void BookInfoByIndex(MyList list)
        {
            Console.WriteLine("Enter index:");
            int index = Convert.ToInt32(Console.ReadLine());
            Book tempBook = new Book(); //will store returned book info
            try
            {
                tempBook = list.ElementAt(index); //saving returned info in tempbook
                Console.WriteLine("Found info: ");
                Console.WriteLine("------------------");
                Console.WriteLine("Name: {0}, Author: {1}, Pages: {2}", tempBook.name, tempBook.author, tempBook.pages);
            }
            catch (Exception)
            {
                Console.WriteLine("No info found. Check index.");
            }
        }
        static void IndexFind(MyList list)
        {
            Console.WriteLine("Creating an object...");
            Console.WriteLine("Enter book name:");
            string bookName = Console.ReadLine();
            Console.WriteLine("Enter book author:");
            string bookAuthor = Console.ReadLine();
            Console.WriteLine("Enter book pages: ");
            int pages = Convert.ToInt32(Console.ReadLine());
            Book book = new Book(bookName, bookAuthor, pages);

            Console.WriteLine("Define positions to insert object in..");
            Console.WriteLine("First position: ");
            int firstPos = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Second position: ");
            int secondPos = Convert.ToInt32(Console.ReadLine());

            //inserting same book in 3 positions
            list.InsertItem(book, firstPos);
            list.InsertItem(book, secondPos);

            Console.WriteLine("First found index of this object: {0}", list.FirstIndexOf(book));
            Console.WriteLine("Last found index of this object: {0}", list.LastIndexOf(book));
            Console.WriteLine("\n");
        }
        static void Main(string[] args)
        {
            MyList list = new MyList();

            Console.WriteLine("MENU");
            Console.WriteLine("=====================");
            int choice = 0;
            do
            {
                Console.WriteLine("Choose your action: \n\n[1] Register books\n[2] Display list of books\n[3] Insert a new book in index position\n[4] Get list element count\n[5] Remove element by index\n[6] Display information about an index book\n[7] Example of finding first and last index of an item\n[8] Clear list\n[9] Exit");
                Console.WriteLine("");
                int userInput = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                switch (userInput)
                {
                    case 1:
                        Book.RegisterData(list);
                        break;
                    case 2:
                        list.PrintList();
                        break;
                    case 3:
                        InsertBook(list);
                        break;
                    case 4:
                        Console.WriteLine("List count: {0}", list.GetCount());
                        break;
                    case 5:
                        RemoveByIndex(list);
                        break;
                    case 6:
                        BookInfoByIndex(list);
                        break;
                    case 7:
                        IndexFind(list);
                        break;
                    case 8:
                        list.Clear();
                        Console.WriteLine("List cleared!\n");
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Theres no such choice");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 9);
        }
    }
}