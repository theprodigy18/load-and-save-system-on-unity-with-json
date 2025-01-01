# Save and Load System on Unity with JSON  

This project demonstrates how to implement a **Save and Load System** in Unity using JSON. It emphasizes readability and modularity by separating concerns into distinct scripts. The system is designed to handle primitive data types and offers strategies for saving complex data types like `Sprite` by converting them into strings (e.g., the sprite's name).  

## Features  
- **Save and Load Primitive Data Types**: Efficiently stores and retrieves data like integers, floats, strings, etc.  
- **Support for Custom Data Types**: Learn how to save complex types like `Sprite` by serializing them into strings.  
- **Separation of Concerns**: Core save and load logic is separated into its own script for better readability and reusability.  
- **JSON-Based**: Utilizes Unity's `JsonUtility` for lightweight and human-readable data serialization.  
- **Encrypt&Decrypt**: Secure your game save file so it can't be edited.  

## Installation  
1. You can download or copy the code to your project.  
2. Edit the code so it will be suited to your project. 

## How It Works  
### 1. Saving Data  
The system serializes your data into JSON format, encrypt it, and writes it to a file. Primitive types are saved directly, while custom data types are converted into savable formats (e.g., `Sprite` to string).  

### 2. Loading Data
The system reads the JSON file, decrypt the data, deserializes the data, and applies it back to the game objects. For complex data like sprites, it retrieves the sprite by its name.

### 3. Tips for Complex Data
**Sprites**: Save their names as strings and load them via Resources.Load<Sprite>().
**Transforms**: Save positions, rotations, or scales as array or list of float.
