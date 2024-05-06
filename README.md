# Potato Price Finder

## Description

The Potato Price Finder is a tool that helps Restaurant Supply Co. find the cheapest providers to buy potatoes from wholesale, so they can provide the lowest prices to their retail customers. The latest potato information for this quarter is fetched from a CSV file and processed to display the three cheapest suppliers in terms of price per pound.

## Instructions for Running the Program

1. **Clone the Repository**: 

2. **Navigate to the Project Directory**: 

3. **Install Dependencies**: npm install

4. **Run the Program**: node potatoPriceFinder.js


5. **Follow the Prompts**: 
Enter the pounds of potatoes available to purchase when prompted.

6. **Sample Input**: 
```Enter pounds of potatoes available to purchase: 1250```

7. **Sample Output**: 
```
Fetching potato data from the server...
Enter pounds of potatoes available to purchase: 1250
User input: 1250 pounds
Downloading CSV file...
CSV file downloaded successfully.
Processing CSV data...
CSV data processed successfully.
Displaying the three cheapest suppliers:
1. Greenhouse Growers - Price per pound: $0.36, Quantity available: 1250 pounds
2. Green Thumb Farms - Price per pound: $0.40, Quantity available: 2700 pounds
3. PotatoCo - Price per pound: $0.40, Quantity available: 1500 pounds.
```

## External Libraries Used:
### axios:
Description: Axios is a promise-based HTTP client for the browser and Node.js. It allows you to make HTTP requests to fetch data from servers.
Link: https://www.npmjs.com/package/axios

### csv-parser:
Description: csv-parser is a streaming parser for parsing CSV files in Node.js. It converts CSV input into arrays or objects.
Link: csv-parser

### readline:
Description: Readline is a built-in Node.js module that provides an interface for reading data from a Readable stream (such as process.stdin) one line at a time.

## References:
### Node.js Documentation:
Description: The Node.js documentation provides detailed information and examples for using the various modules and features of Node.js, including the readline module used for user input.
Link: Node.js Documentation

## Contact Information:
- Author: Aditya Shinde
- Email: s.adityashinde11@gmail.com
