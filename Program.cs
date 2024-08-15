using System;
using System.Collections.Generic;
using System.IO;

namespace FactoryShapes
{
    class Program
    {
        static void Main(string[] args)
        {
            // menu control
            bool showTheMenu = true;

            // the menu item functions
            Canvas canvas = new Canvas();
            User user = new User();
            ShapeClient client = new ShapeClient();
            while (showTheMenu)
            {
                showTheMenu = MainMenu(canvas,user,client);
            }
            Console.Clear();
        }

        // This section contains the application menu
        private static bool MainMenu(Canvas canvas,User user, ShapeClient client)
        {
                Console.Clear();
                HelpCommand(canvas);//Generates help menu
                Console.WriteLine("Canvas created - use commands to add shapes to the canvas");
                //If any of these chars are inputted it does a command.
                string command = UI1();
                switch (command[0])
                {
                    case 'H':
                    case 'h':
                    //Displays commands
                        HelpCommand(canvas);
                        MenuItemClear();
                        return true;
                    case 'A':
                    case 'a':
                    //Adds shape 
                        Add(command.Split(' ')[1],canvas,user,client);
                        MenuItemClear();
                        return true;
                    case 'U':
                    case 'u':
                    //Undos operation
                        user.Undo();
                        MenuItemClear();
                        return true;
                    case 'R':
                    case 'r':
                    //Redos operation
                        user.Redo();
                        MenuItemClear();
                        return true;
                    case 'C':
                    case 'c':
                    //Clears canvas
                        ClearCanvas(canvas);
                        MenuItemClear();
                        return true;
                    case 'D':
                    case 'd':
                    //Displays canvas 
                        DisplayCanvas(canvas);
                        MenuItemClear();
                        return true;
                    case 'S':
                    case 's':
                    //Save canvas to svg file
                        SaveCanvas(canvas);
                        MenuItemClear();
                        return true;
                    case 'Q':
                    case 'q':
                    //Quit application
                        QuitMenu(canvas);
                        MenuItemClear();
                        return false;
                    default:
                    Console.WriteLine("Use Commands listed, type H for the list of commands");
                    MenuItemClear();
                        return true;
                }
        }
        //Quit app method
        private static void QuitMenu(Canvas canvas)
        {
            Console.Clear();
            Console.WriteLine("Goodbye!");
        }
        private static void MenuItemClear()
        {
            // menu header output
            Console.WriteLine("\nHit any key to continue ...\n");
            Console.ReadKey();
        }
        //Add shape method
        private static void Add(string shape,Canvas canvas,User user,ShapeClient client)
        {
            //Creating a shape depending on input
            if(shape.Equals("circle"))
            {
               // CreateRandomCircle(canvas,user);
                client.ClientCode(new CircleFactory(),canvas,user);
                

            }
            else if(shape.Equals("rectangle"))
            {
                //CreateRandomRectangle(canvas,user);
                client.ClientCode(new RectangleFactory(),canvas,user);
            }
            else if(shape.Equals("square"))
            {
                //CreateRandomSquare(canvas,user);
                client.ClientCode(new SquareFactory(),canvas,user);
            }
            else if(shape.Equals("polyline"))
            {
                //CreateRandomPolyline(canvas,user);
                client.ClientCode(new PolylineFactory(),canvas,user);
            }
            else if(shape.Equals("ellipse"))
            {
                //CreateRandomEllipse(canvas,user);
                client.ClientCode(new EllipseFactory(),canvas,user);
            }
            else if(shape.Equals("polygon"))
            {
                //CreateRandomEllipse(canvas,user);
                client.ClientCode(new PolygonFactory(),canvas,user);
            }
            else
            {
              Console.WriteLine("Type a shape in this list!");
              Console.WriteLine("circle");
              Console.WriteLine("rectangle");
              Console.WriteLine("square");
              Console.WriteLine("polyline");
              Console.WriteLine("ellipse");
              Console.WriteLine("polygon");
            }
        }
        static string UI1()
        {
            return Console.ReadLine();
        }
        private static void HelpCommand(Canvas canvas)
        {
           Console.WriteLine("Commands:");
           Console.WriteLine("".PadLeft(6, ' ')+"H              Help - displays this message");
           Console.WriteLine("".PadLeft(6, ' ')+"A"+" <shape>      Add shape to canvas");
           Console.WriteLine("".PadLeft(6, ' ')+"U              Undo last operation");
           Console.WriteLine("".PadLeft(6, ' ')+"R              Redo last operation");
           Console.WriteLine("".PadLeft(6, ' ')+"C              Clear canvas");
           Console.WriteLine("".PadLeft(6, ' ')+"Q              Quit application");
           Console.WriteLine("".PadLeft(6, ' ')+"D              Display canvas");
           Console.WriteLine("".PadLeft(6, ' ')+"S              Save canvas to svg file");
        }
        private static void ClearCanvas(Canvas canvas)
        {
            // menu header output
            Console.Clear();
            Console.WriteLine("Cleared all shapes in Canvas");
            canvas.ClearCanvas();
        }
        private static void DisplayCanvas(Canvas canvas)
        {
            // menu header output
            Console.Clear();
            Console.WriteLine("Displaying Canvas\n");
            canvas.DisplayCanvas();
        }
        private static void SaveCanvas(Canvas canvas)
        {
            Console.Clear();
            Console.WriteLine("Canvas saved to a SVG file");
            canvas.SaveCanvas();
        }
    }
        public class Canvas
        {
            private Stack<IShape> canvas = new Stack<IShape>(); // using stack to store shapes

            public Canvas () { }
            //Remove shape from canvas method
            public IShape Remove()
            {
                IShape s = canvas.Pop();
                Console.WriteLine("Removed Shape from canvas: {0}", s);
                return s;
            }
            //Add shape to canvas method
            public void Add(IShape s) {
               this.canvas.Push(s);
               Console.WriteLine("{0} added to canvas",s);
            }
            //Clear all shapes in canvas 
            public void ClearCanvas(){
                canvas.Clear();
            }
            //Displaying canvas
            public void DisplayCanvas () {
                // create the opening and closing tags for the svg canvas
                string svgOpen = @"<svg height=""600"" width=""600"" xmlns=""http://www.w3.org/2000/svg"">";
                string svgClose = @"</svg>";

                // draw the canvas (write to the display)
                Console.WriteLine(svgOpen);
                // iterate over all shapes in the stack
                foreach (var shapes in canvas) Console.WriteLine("".PadLeft(3, ' ') + shapes.ToSVGElement());
                Console.WriteLine(svgClose);
            }
            public void SaveCanvas () {
                // create the opening and closing tags for the svg canvas
                string svgOpen = @"<svg height=""600"" width=""600"" xmlns=""http://www.w3.org/2000/svg"">";
                string svgClose = @"</svg>";
                // Create a file to write the svg text.
                //Exporting the canvas to a file in SVG.
                string path = @"./Shapes.svg";
                using (StreamWriter sw = File.CreateText(path))
               {
                  sw.WriteLine(svgOpen);
                  foreach (var shapes in canvas) sw.WriteLine("".PadLeft(3, ' ') + shapes.ToSVGElement());
                  sw.WriteLine(svgClose);
               }
            }
        }

        //For the Undo and Redo functionality I used Command Design Pattern

        // The User (Invoker) Class
        public class User
        {
            private Stack<Command> undo;
            private Stack<Command> redo;

            public int UndoCount { get => undo.Count; }
            public int RedoCount { get => undo.Count; }
            public User()
            {
                Reset();
            }
            public void Reset()
            {
                undo = new Stack<Command>();
                redo = new Stack<Command>();
            }

            public void Action(Command command)
            {
                // first update the undo - redo stacks
                undo.Push(command);  // save the command to the undo command
                redo.Clear();        // once a new command is issued, the redo stack clears

                // next determine  action from the Command object type
                // this is going to be AddShapeCommand or DeleteShapeCommand
                Type t = command.GetType();
                if (t.Equals(typeof(AddShapeCommand)))
                {
                    command.Do();
                }
                if (t.Equals(typeof(DeleteShapeCommand)))
                {
                    command.Do();
                }
            }
            // Undo
            public void Undo()
            {
                Console.Clear();
                Console.WriteLine("Undoing operation!"); Console.WriteLine();
                if (undo.Count > 0)
                {
                    Command c = undo.Pop(); c.Undo(); redo.Push(c);
                }
            }
            // Redo
            public void Redo()
            {
                Console.Clear();
                Console.WriteLine("Redoing operation!"); Console.WriteLine();
                if (redo.Count > 0)
                {
                    Command c = redo.Pop(); c.Do(); undo.Push(c);
                }
            }
        }
         // Abstract Command (Command) class - commands can do something and also undo
        public abstract class Command
        {
            public abstract void Do();     // (do)
            public abstract void Undo();   // (undo)
        } 
        // Add Shape Command - it is a ConcreteCommand Class (extends Command)
        // This adds a Shape to the Canvas as the "Do" action
        public class AddShapeCommand : Command
        {
            IShape shape;
            Canvas canvas;

            public AddShapeCommand(IShape s, Canvas c)
            {
                shape = s;
                canvas = c;
            }

            // Adds a shape to the canvas as "Do" action
            public override void Do()
            {
                canvas.Add(shape);
            }
            // Removes a shape from the canvas as "Undo" action
            public override void Undo()
            {
                shape = canvas.Remove();
            }
        } 
        // Delete Shape Command - it is a ConcreteCommand Class (extends Command)
        // This deletes a Shape from the Canvas as the "Do" action
        public class DeleteShapeCommand : Command
        {

            IShape shape;
            Canvas canvas;

            public DeleteShapeCommand(Canvas c)
            {
                canvas = c;
            }

            // Removes a shape from the canvas as "Do" action
            public override void Do()
            {
                shape = canvas.Remove();
            }

            // Restores a shape to the canvas a an "Undo" action
            public override void Undo()
            {
                canvas.Add(shape);
            }
        }
    // I'm implementing the Factory method design pattern for creating shapes 
    public abstract class ShapeCreator
    {
        // The Creator may also provide some default implementation of
        // the factory method.
        //Factory method ishape to create object
        public abstract IShape FactoryMethod();
        public string SomeOperation()
        {
            // Call the factory method to create a Product object.
            var shape = FactoryMethod();
            // Now, use the product.
            var result =  shape.Operation();
            return result;
        }
    }
    // Concrete Creators override the factory method in order to change the
    // resulting product's type.
    class SquareFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            //create the random square
            Random rnd = new Random(); // random number generator
            Square s = new Square("S"+rnd.Next(1, 50),rnd.Next(100, 300), rnd.Next(100, 300), rnd.Next(100, 300));
            return s;
        }
    }

    class CircleFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            // create the random circle
            Random rnd = new Random(); // random number generator
            Circle c = new Circle("C"+rnd.Next(1, 50),rnd.Next(100, 200), rnd.Next(100, 200), rnd.Next(1, 100));
            return c;
        }
    }
    class RectangleFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            //create the random rectangle
            Random rnd = new Random(); // random number generator
            Rectangle r = new Rectangle("R"+rnd.Next(1, 50),rnd.Next(100, 300), rnd.Next(100, 300), rnd.Next(100, 200), rnd.Next(100, 200));
            return r;
        }
    }
    class PolylineFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            // create the random polyline
            Random rnd = new Random(); // random number generator
            Polyline pl = new Polyline("PL"+rnd.Next(1, 50),rnd.Next(60, 65)+" "+ rnd.Next(100, 250)+" "+rnd.Next(65, 70)+" "+rnd.Next(100, 250)+" "+rnd.Next(70, 75)+" "+rnd.Next(100, 250)+" "+rnd.Next(75, 80)+" "+rnd.Next(100, 250)+" "+rnd.Next(80, 85)+" "+rnd.Next(100, 250)+" "+rnd.Next(85, 90)+" "+rnd.Next(100, 250)+" "+rnd.Next(90, 95)+" "+rnd.Next(100, 250)+" "+rnd.Next(95, 100)+" "+rnd.Next(100, 250)+" "+rnd.Next(100, 110)+" "+rnd.Next(100, 250));
            return pl;
        }
    }
    class EllipseFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            //create the random ellipse
            Random rnd = new Random(); // random number generator
            Ellipse e = new Ellipse("E"+rnd.Next(1, 50),rnd.Next(100, 200), rnd.Next(1, 100), rnd.Next(1, 100), rnd.Next(1, 100));
            return e;
        }
    }
    class PolygonFactory : ShapeCreator
    {
        public override IShape FactoryMethod()
        {
            // create the random polygon
            Random rnd = new Random(); // random number generator
            Polygon pg = new Polygon("PG"+rnd.Next(1, 50),rnd.Next(50, 60)+" "+ rnd.Next(160, 170)+" "+rnd.Next(80, 90)+" "+rnd.Next(160, 170)+" "+rnd.Next(100, 110)+" "+rnd.Next(130, 140)+" "+rnd.Next(80, 90)+" "+rnd.Next(100, 110)+" "+rnd.Next(50, 60)+" "+rnd.Next(100, 110)+" "+rnd.Next(30, 40)+" "+rnd.Next(130, 140));
            return pg;
        }
    }
    // The IShape interface declares the operations that all concrete shapes must implement

    public interface IShape
    {
        public string Operation();
        public string ToSVGElement();
    }
    // Concrete Shapes provide various implementations of the IShape
    // interface.
    //Square inherited class
    class Square : IShape
    {
        public string ID { get; set; }            // shapes have an ID
        //public abstract string ToSVGElement();    // shapes must implement to SVG method

        public int X { get; set; }        // Square centre x-coordinate
        public int Y { get; set; }        // Square centre y-coordinate
        public int L { get; set; }        // Square length
        
        //created a style object to be utilized to create shape's style
        Style styleobj = new StyleObj(new RedColor(),new RoundStyle());

        public Square() { ID="S100"; X = 100; Y = 100; L = 100; }
        public Square(string id, int x, int y, int l) 
        { ID = id; X = x; Y = y; L = l;}

        public override string ToString()
        {
            // convert the Object to a string
            string dispXYR = String.Format("Square ({3}) at (X={0},Y={0},L={2})", X, Y, L, ID);
            // return the convered string
            return dispXYR;
        }

        public string ToSVGElement()
        {
            // convert the object to an SVG element descriptor string for square
            string dispSVG = String.Format(@"<rect id=""{3}"" x=""{0}"" y=""{0}"" width=""{2}"" height=""{2}"" stroke=""black"" stroke-width=""5"" fill=""{4}"" stroke-linejoin=""{5}"" />", X, Y, L, ID,styleobj.drawC(),styleobj.drawS());
            return dispSVG;
            // 
        }
        public string Operation()
        {
            return "SQUARE";
        }
    }
    //Circle inherited class
    class Circle : IShape
    {
        public string ID { get; set; }
        public int X { get; set; }        // circle centre x-coordinate
        public int Y { get; set; }        // circle centre y-coordinate
        public int R { get; set; }        // circle radius

        //created a style object to be utilized to create shape's style
        Style styleobj = new StyleObj(new YellowColor(),new RoundStyle());
        public Circle() { ID="C100"; X = 200; Y = 200; R = 100; }
        public Circle(string id, int x, int y, int r) { ID = id; X = x; Y = y; R = r; }

        public override string ToString()
        {
            // convert the Object to a string
            string dispXYR = String.Format("Circle ({3}) at (X={0},Y={1},R={2})", X, Y, R, ID);
            // return the convered string
            return dispXYR;
        }

        public string ToSVGElement()
        {
            // convert the object to an SVG element descriptor string for circle
            string dispSVG = String.Format(@"<circle id=""{3}"" cx=""{0}"" cy=""{1}"" r=""{2}"" stroke=""black"" stroke-width=""5"" fill=""{4}"" stroke-linejoin=""{5}"" />", X, Y, R, ID,styleobj.drawC(),styleobj.drawS());
            return dispSVG;
        }
        public string Operation()
        {
            return "CIRCLE";
        }
    }
    //Rectangle inherited class
    class Rectangle : IShape
    {
        public string ID { get; set; }
        public int X { get; set; }        // rectangle centre x-coordinate
        public int Y { get; set; }        // rectangle centre y-coordinate
        public int W { get; set; }        // rectangle width
        public int H { get; set; }        // rectangle heigth
        
        //created a style object to be utilized to create shape's style
        Style styleobj = new StyleObj(new OrangeColor(),new SquareStyle());
        public Rectangle() { ID="R100"; X = 100; Y = 100; W = 100; H = 100; }
        public Rectangle(string id, int x, int y, int w, int h) { ID = id; X = x; Y = y; W = w; H = h; }

        public override string ToString()
        {
            // convert the Object to a string
            string dispXYR = String.Format("Rectangle ({4}) at (X={0},Y={0},W={2},H={3})", X, Y, W, H, ID);
            // return the convered string
            return dispXYR;
        }

        public string ToSVGElement()
        {
            // convert the object to an SVG element descriptor string for rectangle
            string dispSVG = String.Format(@"<rect id=""{4}"" x=""{0}"" y=""{0}"" width=""{2}"" height=""{3}"" stroke=""black"" stroke-width=""5"" fill=""{5}"" stroke-linejoin=""{6}"" />", X, Y, W, H, ID,styleobj.drawC(),styleobj.drawS());
            return dispSVG;
        }
        public string Operation()
        {
            return "RECTANGLE";
        }
    }
    //Polyline inherited class
    class Polyline : IShape
    {
        public string ID { get; set; }
        public string Points { get; set; } // polyline points

        //created a style object to be utilized to create shape's style
        Style styleobj = new StyleObj(new GreenColor(),new SquareStyle());
        public Polyline() { ID="PL100"; Points = "60 110 65 120 70 115 75 130 80 125 85 140 90 135 95 150 100 145"; }
        public Polyline(string id, string points) { ID = id; Points = points; }

        public override string ToString()
        {
            // convert the Object to a string
            string dispXYR = String.Format("Polyline ({1}) at ({0})", Points, ID);
            // return the convered string
            return dispXYR;
        }

        public string ToSVGElement()
        {
            // convert the object to an SVG element descriptor string for polyline
            string dispSVG = String.Format(@"<polyline id=""{1}"" points=""{0}"" stroke=""{2}"" fill=""transparent"" stroke-width=""5"" stroke-linejoin=""{3}"" />", Points, ID,styleobj.drawC(),styleobj.drawS());
            return dispSVG;
        }
        public string Operation()
        {
            return "POLYLINE";
        }
    }
    //Ellipse inherited class
    class Ellipse : IShape
    {
        public string ID { get; set; }
        public int X { get; set; }        // ellipse centre x-coordinate
        public int Y { get; set; }        // ellipse centre y-coordinate
        public int RX { get; set; }       // ellipse x radius
        public int RY { get; set; }       // ellipse y radius
        
        //created a style object to be utilized to create shape's style
        Style styleobj = new StyleObj(new BlueColor(),new RoundStyle());
        public Ellipse() { ID="E100"; X = 100; Y = 100; RX = 100; RY = 100; }
        public Ellipse(string id, int x, int y, int rx, int ry) { ID = id; X = x; Y = y; RX = rx; RY = ry; }

        public override string ToString()
        {
            // convert the Object to a string
            string dispXYR = String.Format("Ellipse ({4}) at (X={0},Y={0},RX={2},RY={3})", X, Y, RX, RY, ID);
            // return the convered string
            return dispXYR;
        }
        public string ToSVGElement()
        {
            // convert the object to an SVG element descriptor string for ellipse
            string dispSVG = String.Format(@"<ellipse id=""{4}"" cx=""{0}"" cy=""{0}"" rx=""{2}"" ry=""{3}"" stroke=""black"" stroke-width=""5"" fill=""{5}"" stroke-linejoin=""{6}"" />", X, Y, RX, RY, ID,styleobj.drawC(),styleobj.drawS());
            return dispSVG;
        }
        public string Operation()
        {
            return "ELLIPSE";
        }
    }
    //Polygon inherited class
    class Polygon : IShape
    {
        public string ID { get; set; }
        public string Points { get; set; }        // polygon points
        
        //created a style object to be utilized to create shape's style
        Style styleobj = new StyleObj(new PurpleColor(),new RoundStyle());
        public Polygon() { ID="PG100"; Points = "50 160 55 180 70 180 60 190 65 205 50 195 35 205 40 190 30 180 45 180"; }
        public Polygon(string id, string points) { ID = id; Points = points; }
        public override string ToString()
        {
            // convert the Object to a string
            string dispXYR = String.Format("Polygon ({1}) at ({0}).", Points, ID);
            // return the convered string
            return dispXYR;
        }
        public string ToSVGElement()
        {
            // convert the object to an SVG element descriptor string for polygon
            string dispSVG = String.Format(@"<polygon id=""{1}"" points=""{0}"" stroke=""black"" fill=""{2}"" stroke-width=""5"" stroke-linejoin=""{3}"" />", Points, ID,styleobj.drawC(),styleobj.drawS());
            return dispSVG;
        }
        public string Operation()
        {
            return "POLYGON";
        }
    }  

    class ShapeClient
    {
        // The client code works with an instance of a concrete creator, albeit
        // through its base interface.
        public void ClientCode(ShapeCreator creator,Canvas canvas,User user)
        {
            Console.Clear();
            Console.WriteLine("Created a Random {0}\n",creator.SomeOperation());
            // add the shape to the canvas - on top of the stack
            user.Action(new AddShapeCommand(creator.FactoryMethod(),canvas));

        }
    }
    // Using Bridge design pattern to create style objects
    // Colour interface
    public interface Color
    {
        public string showShapeColor();
    }
    //Style interface
    public interface BorderLineStyle
    {
        public string showShapeStyle();
    } 
    public class SquareStyle : BorderLineStyle
    {
        public string showShapeStyle()
        {
            string style = "square";
            return style;
        }
    }
    public class RoundStyle : BorderLineStyle
    {
        public string showShapeStyle()
        {
            string style = "round";
            return style;
        }
    }
    public class BlueColor : Color
    {
        public string showShapeColor()
        {
            string color = "blue";
            return color;
        }
    }
    public class RedColor : Color
    {
        public string showShapeColor()
        {
            string color = "red";
            return color;
            
        }
    }
    public class YellowColor : Color
    {
        public string showShapeColor()
        {
            string color = "yellow";
            return color;
            
        }
    }
    public class GreenColor : Color
    {
        public string showShapeColor()
        {
            string color = "limegreen";
            return color;
        }
    }
    public class OrangeColor : Color
    {
        public string showShapeColor()
        {
            string color = "orange";
            return color;
        }
    }
    public class PurpleColor : Color
    {
        public string showShapeColor()
        {
            string color = "purple";
            return color;
        }
    }

    public abstract class Style
    {
        protected Color color;
        protected BorderLineStyle style;

        public Style(Color color,BorderLineStyle style)
        {
            this.color = color;
            this.style = style;
        }

        public abstract string drawC(); //string method for drawing color
        public abstract string drawS(); //string method for drawing style
    }
    //inherited class for style object 
    public class StyleObj : Style
    {

        public StyleObj(Color color,BorderLineStyle style) : base(color,style)
        {
        }

        public override string drawC()
        {
            string c = color.showShapeColor();
            return c;
        }
        public override string drawS()
        {
            string s = style.showShapeStyle();
            return s;
        }
    }
    
}
//Operating System: Windows
//IDE used VS Code.
