# Library_management

A library management system project for my OOP class.

Design and implement an object-oriented library management system. The basic assumptions are given below:

System should contain core objects: 
Book (describes the single book in the library)
Category (contains books from a specific category, such as comedies, dramas, fairy tales, etc.), 
Catalogue (stores library catalogue by category)
Order (describes a single user's borrow)
Receipt (describes the receipt for the order: full invoice or simple receipt)
Application (the main object that provides interaction with the user)
Inheritance: design descendant objects from the Book object type that are book types, e.g. Dictionary, Encyclopedia, OperationManual, etc. You can also design Invoice and Receipt descendant objects from the BaseReceipt object.
Enable basic operations on Books, Categories, Catalogue, Orders and Receipts. Basic operations include:
Adding object/collection of objects
Deleting object/collection of objects
Displaying object/collection of objects
Searching/Filtering object/collection of objects
Additional functions of the program (for a score above 4.0)
Design and implement interfaces. Base the entire system on the interfaces principle
Perform write and read to file(s) (data persistence)
Use "Dependency Injection" mechanisms, e.g. Singleton, Factory, Decorator (e.g. decorators for implementing operators “and”/”or” when searching)
