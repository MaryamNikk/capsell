const DynamicJsonContract = artifacts.require("DynamicJsonContract");
const Redis = require('ioredis');

const redis = new Redis({
  host: 'localhost',
  port: 6379,
});

async function deployContract(deployer) {
  try {
    var initialInvoice = await redis.get('initialInvoice');

    if (!initialInvoice) {
      console.error('No data found in the cache. Please save data to cache first.');
      initialInvoice = '{"example": "initial data"}';
    }

    console.log(initialInvoice);

    // Deploy the contract with the initialInvoice data
    await deployer.deploy(DynamicJsonContract, initialInvoice);
  } catch (error) {
    console.error(error);
  }
}

// Use the wrapper function to deploy the contract
module.exports = async function (deployer) {
  await deployContract(deployer);
};
