# JSONDataManagementTool

## Overview

The JSON Data Management Application is a C# solution designed to handle and manipulate data stored in JSON files. This project is composed of two main components:

1. **Class Library**: Defines data structures and JSON parsing functionality.
2. **Console Application**: Provides an interactive user interface to work with JSON data.

## Purpose

The application enables users to:

- **Read and Write JSON Data**: Manage JSON data through a class library that handles serialization and deserialization.
- **Filter and Sort Data**: Provide tools for filtering and sorting JSON data based on user-defined criteria.
- **User Interface**: Interact with the data via a console application with a straightforward menu-driven interface.

## Class Library

### Components

1. **Data Class (`MyType`)**

   - Represents the data structure defined in the JSON file.
   - Fields are read-only and initialized via a constructor.
   - The class is designed to encapsulate data and adhere to object-oriented design principles.

2. **Static Class (`JsonParser`)**

   - Contains static methods for handling JSON data:
     - `WriteJson`: Serializes data objects to JSON format.
     - `ReadJson`: Deserializes JSON data into data objects.
   - Utilizes `System.Console` for data handling and avoids specialized JSON libraries.

### Design Principles

- **Encapsulation**: Ensures data is protected and accessible only through defined methods.
- **Single Responsibility Principle**: Each class has a specific role and responsibility.
- **Liskov Substitution Principle**: Derived classes can be used interchangeably with base classes.
- **Dependency Inversion Principle**: High-level modules depend on abstractions rather than concrete implementations.

## Console Application

### Features

1. **Data Input**

   - Allows users to input JSON data via `System.Console` or specify a file path.
   - Handles data reading and writing through `JsonParser` methods.

2. **Data Operations**

   - **Filtering**: Users can filter data based on selected fields.
   - **Sorting**: Users can sort data by selected fields.
   - **Output**: Data can be displayed on the console or saved to a file. Options are provided to overwrite existing files or save to a new file.

3. **User Interface**

   - Provides a menu-driven interface for:
     - Inputting data from the console or a file.
     - Filtering and sorting data.
     - Saving or displaying data.
   - Handles file operations, including error checking and resource management.

### Interaction

- **Console Handling**: Supports redirection of `System.Console` streams for file operations.
- **Data Integrity**: Ensures that all created JSON files adhere to the same structure as the input files.

## How to Use

1. **Run the Console Application**

   - Start the console application to access the main menu.
   - Choose to input data either from the console or a file.

2. **Filter and Sort Data**

   - Use the menu options to filter and sort data based on your criteria.
   - Review the results directly in the console or save them to a file.

3. **Save or Display Data**

   - Choose to save the results to a file or display them on the console.
   - Specify file paths or choose to overwrite existing files as needed.

## Development Notes

- **Error Handling**: The application includes mechanisms for validating user input and managing exceptions.
- **Coding Standards**: Follow C# and .NET 6.0 coding conventions and best practices.
- **Dependencies**: The project avoids third-party libraries and NuGet packages, relying solely on .NET framework capabilities.
