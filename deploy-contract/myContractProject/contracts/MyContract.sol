// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract DynamicJsonContract {
    string public invoice;

    constructor(string memory initialInvoice) {
        invoice = initialInvoice;
    }

    function updateJsonData(string memory newInvoice) public {
        invoice = newInvoice;
    }
}
