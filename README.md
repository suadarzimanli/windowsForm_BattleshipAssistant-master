# Battleship Assistant

This is a C# Windows Forms application for playing the Battleship game with a login functionality. The game allows users to register, log in, and play battleship against a computer-controlled bot. The user data is stored in a Microsoft SQL Server database.

## Features

- User registration and login
- Battleship game against a computer-controlled bot
- Randomized ship placement for the bot
- Randomized shooting by the bot
- Microsoft SQL Server integration for user data storage

## Installation

1. Clone the repository to your local machine using the following command:

2. Open the solution file `BattleshipAssistant.sln` in Visual Studio.

3. Set up a Microsoft SQL Server instance and create a new database.

4. Update the connection string in the `app.config` file with the appropriate details for your SQL Server instance.

5. Build the solution to restore NuGet packages and compile the project.

6. Start the application by running the project.

## Usage

1. Launch the application and you will be presented with a login screen.

2. If you don't have an account, click on the "Register" link and fill in the required details. The name, email, and password will be stored in the SQL database.

3. After registering or if you already have an account, enter your username and password and click on the "Login" button.

4. The application will check the entered credentials against the user records in the SQL database. If the name or password is incorrect, an error message will be displayed.

5. If the login is successful, the Battleship game will start with a game board.

6. The bot will randomly place its ships on the board, and you need to guess the location of the bot's ships by clicking on the buttons.

7. The bot will also randomly shoot at your ships, and you need to protect your ships by guessing the bot's shooting location.

8. The game continues until all ships on either side are destroyed.

## Contributing

Contributions to the Battleship Assistant project are welcome! If you would like to contribute, please follow these steps:

1. Fork the repository on GitHub.

2. Create a new branch with a descriptive name for your feature or bug fix.

3. Make the necessary changes and commit them to your branch.

4. Push your changes to your forked repository.

5. Submit a pull request to the main repository.

## Acknowledgments

- The Battleship Assistant project is inspired by the classic Battleship game.


