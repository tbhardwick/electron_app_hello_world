# Electron App Hello World

A simple Electron application demonstrating how to build a cross-platform desktop application using Electron.js, HTML, CSS, and JavaScript.

## Description

This is a minimal Electron application that serves as a starting point for building desktop applications. It displays a simple window with a "Hello from Electron renderer!" message.

## Features

- Cross-platform desktop application (Windows, macOS, Linux)
- Simple and lightweight
- Development environment with hot reloading

## Prerequisites

Before you begin, ensure you have the following installed:
- [Node.js](https://nodejs.org/) (v12 or higher)
- npm (comes with Node.js)

## Installation

Clone the repository and install dependencies:

```bash
# Clone this repository
git clone https://github.com/yourusername/electron_app_hello_world.git

# Go into the repository
cd electron_app_hello_world

# Install dependencies
npm install
```

## Usage

This application has two main scripts:

```bash
# Run the application in normal mode
npm start

# Run the application in development mode with hot reloading
npm run dev
```

### Development Mode

The development mode uses `electronmon` to watch for file changes and automatically reload the application. This makes development much faster as you don't need to manually restart the application after each change.

## Project Structure

- `main.js` - Main process file that creates the Electron application window
- `index.html` - HTML file that defines the content displayed in the application window
- `package.json` - Project configuration and dependencies

## Building for Production

To build the application for production, you can add the following scripts to your package.json:

```json
"scripts": {
  "build": "electron-builder",
  "build:mac": "electron-builder --mac",
  "build:win": "electron-builder --win",
  "build:linux": "electron-builder --linux"
}
```

Note: You'll need to install `electron-builder` as a dev dependency:

```bash
npm install --save-dev electron-builder
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Author

Brad Hardwick

---

This project was created using Electron, a framework for creating native applications with web technologies like JavaScript, HTML, and CSS. 