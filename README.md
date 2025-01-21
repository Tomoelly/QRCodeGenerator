# QR Code Generator Console App

This is a C# console application built with **.NET 8**, designed to generate QR Code images based on database records. The application retrieves data from an SQL Server database, creates QR Code images for specific IDs, and saves them as PNG files on your desktop. It is highly configurable and easy to use.

## Features

- Retrieve URLs or other data from an SQL Server database.
- Generate QR Code images using the [QRCoder](https://github.com/codebude/QRCoder) library.
- Save QR Codes as PNG files in a designated folder.
- Use `appsettings.json` for flexible configuration, including database connection strings.
- Robust error handling for database queries and QR Code generation.

## Technologies Used

- **C# (.NET 8)**: Core language for development.
- **QRCoder**: Library for generating QR Code images.
- **Microsoft.Extensions.Configuration**: For managing configuration settings.
- **SQL Server**: Database for storing and retrieving records.

## Prerequisites

Before running the application, ensure you have the following:

- **.NET 8 SDK**: [Download here](https://dotnet.microsoft.com/download/dotnet/8.0).
- **SQL Server**: Ensure you have a running SQL Server instance with the required database and table structure.
- **QRCoder NuGet Package**: Installed automatically via `dotnet restore`.

## Getting Started

Follow these steps to set up and run the application:

### 1. Clone the Repository

```bash
git clone https://github.com/<your-username>/<your-repository-name>.git
cd <your-repository-name>
```

### 2. Configure the Application

- Open the `appsettings.json` file and update the `DatabaseConnection` value with your SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DatabaseConnection": "Server=<YourServer>; Database=<YourDatabase>; User Id=<YourUsername>; Password=<YourPassword>;"
  }
}
```

Replace `<YourServer>`, `<YourDatabase>`, `<YourUsername>`, and `<YourPassword>` with your actual database details.

### 3. Restore Dependencies

Run the following command to restore NuGet packages:

```bash
dotnet restore
```

### 4. Run the Application

Start the application using:

```bash
dotnet run
```

### 5. Usage Instructions

1. When prompted, enter the **start ID** and **end ID** for the range of database records you want to generate QR Codes for.
2. The application will retrieve the corresponding data from the database and generate QR Codes.
3. Generated QR Code images will be saved in the `QRCodeImages` folder on your desktop.

### Example Output

Generated QR Codes are saved as PNG files in the format:

```
QRCode_<ID>.png
```

For example, if the range is `1-3`, the output will include:
- `QRCode_1.png`
- `QRCode_2.png`
- `QRCode_3.png`

## Error Handling

- Missing or invalid configuration in `appsettings.json` is detected, and a meaningful error message is displayed.
- Database connection or query failures are logged and reported to the console.
- Issues during QR Code generation are captured and logged.

## Extending the Project

### Potential Enhancements

- Support for parallel processing to improve batch performance.
- Customization of QR Code styles (e.g., colors, logo overlays).
- Integration with cloud storage (e.g., Azure Blob Storage, AWS S3) for saving QR Codes.
- Command-line arguments to skip prompts and automate batch generation.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Feel free to fork this repository, open issues, or submit pull requests.

## Acknowledgments

- [QRCoder Library](https://github.com/codebude/QRCoder) for providing a robust QR Code generation solution.
- Special thanks to ChatGPT for guidance and support. 🙌 (This is my personal thank you, not added by ChatGPT itself—it's me genuinely thanking ChatGPT!)
