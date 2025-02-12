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
-- NOTE: "Order" can be a reserved keyword, depending on your SQL dialect.
-- To avoid conflicts, you can either quote the table name or rename it to Orders.
CREATE TABLE `Orders` (
    order_id         INT         NOT NULL,
    supplier_id      INT         NOT NULL,
    product_id       INT         NOT NULL,
    order_date       DATE,
    quantity_ordered INT         DEFAULT 1,
    PRIMARY KEY (order_id),
    FOREIGN KEY (supplier_id) REFERENCES Supplier(supplier_id),
    FOREIGN KEY (product_id)  REFERENCES Product(product_id)
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
    PRIMARY KEY (shipment_id),
    FOREIGN KEY (order_id) REFERENCES `Order`(order_id)
);
