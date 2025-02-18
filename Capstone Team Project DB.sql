DROP DATABASE IF EXISTS `scdb`;
CREATE DATABASE `scdb`;
USE `scdb`;



/*************************************************************************/
/*                   SUPPLIER MANAGEMENT                                 */
/*        Managing relationships with suppliers, procurement, and        */
/*                           sourcing.                                   */
/*************************************************************************/
CREATE TABLE Supplier (
    supplier_id     INT         NOT NULL,
    name            VARCHAR(50) NOT NULL,
    address         VARCHAR(100),
    contact_person  VARCHAR(50),
    phone_number    VARCHAR(20),
    PRIMARY KEY (supplier_id)
);


/*************************************************************************/
/*                   INVENTORY MANAGEMENT                                */
/*  Track and manage inventory levels to ensure optimal stock levels.    */
/*************************************************************************/
CREATE TABLE Product (
    product_id          INT         NOT NULL,
    name                VARCHAR(50) NOT NULL,
    description         VARCHAR(255),
    unit_price          DECIMAL(10, 2),
    quantity_available  INT         DEFAULT 0,
    PRIMARY KEY (product_id)
);


/*************************************************************************/
/*                    ORDER MANAGEMENT                                   */
/*    Managing order creation, tracking, processing, and delivery.       */
/*************************************************************************/
CREATE TABLE `Orders` (
    order_id         INT         NOT NULL,
    supplier_id      INT         NOT NULL,
    product_id       INT         NOT NULL,
    order_date       DATE,
    quantity_ordered INT         DEFAULT 1,
    user_id	     INT	 NOT NULL,
    PRIMARY KEY (order_id),
    FOREIGN KEY (supplier_id) REFERENCES Supplier(supplier_id),
    FOREIGN KEY (product_id)  REFERENCES Product(product_id),
    FOREIGN KEY (user_id) 	  REFERENCES Customers(user_id)
);


/*************************************************************************/
/*                   LOGISTICAL MANAGEMENT                               */
/*    Managing the planning, tracking, and demands of goods and          */
/*           materials within the supply chain.                          */
/*************************************************************************/
CREATE TABLE Shipment (
    shipment_id             INT         NOT NULL,
    order_id                INT         NOT NULL,
    shipment_date           DATE,
    estimated_arrival_date  DATE,
    actual_arrival_date     DATE,
    user_id		    INT         NOT NULL,
    PRIMARY KEY (shipment_id),
    FOREIGN KEY (order_id) REFERENCES `Orders`(order_id),
    FOREIGN KEY (user_id) 	  REFERENCES Customers(user_id)
);


/*************************************************************************/
/*                   CUSTOMER AUTHENTICATION                             */
/*    Table to hold all customer information and customer id	         */
/*           to allow customers to create accounts                       */
/*************************************************************************/
CREATE TABLE Customers (
    cust_first				VARCHAR(30) NOT NULL,
    cust_last				VARCHAR(30) NOT NULL,
    user_name				VARCHAR(20) NOT NULL,
    user_password			VARCHAR(50) NOT NULL,
    user_id				INT         DEFAULT 1,
	
    
    PRIMARY KEY (user_id)
    
);

/*************************************************************************/
/*                   EMPLOYEE AUTHENTICATION                             */
/*    Employee information such as full name, username, password,        */
/*           Employee ID, and Administration level (if applicable)       */
/*************************************************************************/

CREATE TABLE  Employees(
    employee_first 			VARCHAR(30) NOT NULL,
    employee_last 			VARCHAR(30) NOT NULL,
    user_name				VARCHAR(20) NOT NULL,
    user_password			VARCHAR(50) NOT NULL,
    employee_id 			INT 	    DEFAULT 1,
    
    PRIMARY KEY (employee_id)
);
