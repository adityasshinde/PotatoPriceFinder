const axios = require('axios');
const csvParser = require('csv-parser');
const readline = require('readline');

const csvUrl = 'http://eportal.aa-engineers.com/assessment/potatoQ12024.csv';

async function main() {
    try {
        console.log("Fetching potato data from the server...");
        const poundsOfPotatoes = await getUserInput('Enter pounds of potatoes available to purchase: ');
        console.log(`User input: ${poundsOfPotatoes} pounds`);

        console.log("Downloading CSV file...");
        const response = await axios.get(csvUrl, { responseType: 'stream' });
        console.log("CSV file downloaded successfully.");

        console.log("Processing CSV data...");
        const cheapestSuppliers = await filterCheapestSuppliers(response, poundsOfPotatoes);
        console.log("CSV data processed successfully.");

        console.log("Displaying the three cheapest suppliers:");
        displayCheapestSuppliers(cheapestSuppliers);
    } catch (error) {
        console.error("An error occurred:", error.message);
    }
}

async function getUserInput(prompt) {
    const rl = readline.createInterface({
        input: process.stdin,
        output: process.stdout
    });

    return new Promise((resolve) => {
        rl.question(prompt, (input) => {
            rl.close();
            resolve(parseInt(input));
        });
    });
}

async function filterCheapestSuppliers(response, poundsOfPotatoes) {
    const stream = response.data.pipe(csvParser());
    const cheapestSuppliers = [];

    for await (const row of stream) {
        const { name, 'unit weight': weight, 'unit price': unitPrice, 'unit quanitiy': quantity } = row;
        if (parseInt(quantity) >= poundsOfPotatoes) {
            cheapestSuppliers.push({ name, weight: parseFloat(weight), unitPrice: parseFloat(unitPrice), quantity: parseInt(quantity) });
        }
    }

    return cheapestSuppliers
        .sort((a, b) => (a.unitPrice / a.weight) - (b.unitPrice / b.weight))
        .slice(0, 3);
}

function displayCheapestSuppliers(suppliers) {
    suppliers.forEach((supplier, index) => {
        const { name, weight, unitPrice, quantity } = supplier;
        const serialNumber = index + 1;
        console.log(`${serialNumber}. ${name} - Price per pound: $${(unitPrice / weight).toFixed(2)}, Quantity available: ${quantity} pounds`);
    });
}


main();
