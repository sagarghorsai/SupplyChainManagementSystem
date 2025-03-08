using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
	public class StoreData : IStoreData
	{
		private readonly ISqlDataAccess _db;
		public List<CartItem> Cart { get; private set; }

		public StoreData(ISqlDataAccess db)
		{
			Cart = new List<CartItem>();
			_db = db;
		}

		// Fetch Available Products
		public async Task<List<ProductModel>> GetAvailableProducts()
		{
			string sql = "SELECT * FROM Product WHERE Quantity_available > 0";
			return await _db.LoadData<ProductModel, dynamic>(sql, new { });
		}

		// Add to Cart
		public async Task AddToCart(ProductModel product, int quantity, int supplierId)
		{
			if (product.Quantity_available < quantity)
			{
				throw new Exception("Not enough stock available.");
			}

			var existingItem = Cart.FirstOrDefault(x => x.ProductId == product.Product_id);
			if (existingItem != null)
			{
				existingItem.Quantity += quantity;
			}
			else
			{
				Cart.Add(new CartItem
				{
					ProductId = product.Product_id,
					Name = product.Name,
					UnitPrice = product.Unit_price,
					Quantity = quantity,
					supplier_id = product.Supplier_Id // Ensure supplier_id is passed and set correctly
				});


			}

			await Task.CompletedTask; // Ensure it's async
			Console.WriteLine(product.Unit_price);
			Console.WriteLine($"Added product {product.Name} with ProductId {product.Product_id} to cart.");
			Console.WriteLine($"supplier_id for this product: {supplierId}");
		}


		public decimal GetTotal()
		{
			return Cart.Sum(x => x.UnitPrice * x.Quantity);
		}

		public async Task<List<CartItem>> GetCartItems()
		{
			return await Task.FromResult(Cart);  // Or fetch from database if necessary
		}

		// Remove from Cart
		public async Task RemoveFromCart(int productId)
		{
			var item = Cart.FirstOrDefault(x => x.ProductId == productId);
			if (item != null)
			{
				Cart.Remove(item);
			}
			await Task.CompletedTask; // Ensure it's async
		}

		// Save Order in Database
		// Save Order in Database
		public async Task SaveOrder(int customerId, int supplierId)
		{
			if (Cart.Count == 0)
			{
				throw new Exception("Cart is empty. Add items before placing an order.");
			}

			// Verify if the supplier exists
			string checkSupplierSql = "SELECT COUNT(*) FROM Supplier WHERE supplier_id = @SupplierId";
			var supplierExists = await _db.LoadData<int, dynamic>(checkSupplierSql, new { SupplierId = supplierId });

			if (supplierExists.FirstOrDefault() == 0)
			{
				throw new Exception($"Supplier with id {supplierId} does not exist.");
			}

			// Calculate total amount from the cart
			// Calculate total amount from the cart
			decimal totalAmount = GetTotal();

			// Insert the order into the Orders table, including total_amount
			string orderSql = @"
		INSERT INTO Orders (user_id, order_date, supplier_id, total_amount) 
		OUTPUT INSERTED.order_id 
		VALUES (@UserId, GETDATE(), @SupplierId, @TotalAmount)"; // Add @TotalAmount parameter

			// Pass parameters including the total_amount
			var parameters = new
			{
				UserId = customerId,
				SupplierId = supplierId, // Use the actual supplier ID
				TotalAmount = totalAmount // Pass total amount
			};

			// Save the order and get the generated order ID
			int orderId = await _db.SaveDataAndReturnId(orderSql, parameters);


			if (orderId == 0)
			{
				throw new Exception("Order could not be inserted.");
			}

			// Now save the order details (products in the cart)
			string orderDetailsSql = @"
			INSERT INTO OrderDetails (order_id, product_id, quantity_ordered, price)
			VALUES (@OrderId, @ProductId, @QuantityOrdered, @UnitPrice)";

			foreach (var item in Cart)
			{
				if (item.ProductId == 0) // Check for invalid ProductId
				{
					throw new Exception($"Invalid ProductId: {item.ProductId} for product {item.Name}");
				}

				await _db.SaveData(orderDetailsSql, new
				{
					OrderId = orderId,
					ProductId = item.ProductId, // Ensure this is not null
					QuantityOrdered = item.Quantity,
					UnitPrice = item.UnitPrice
				});
			}

			// Update stock in the Products table
			string updateStockSql = "UPDATE Product SET Quantity_available = Quantity_available - @QuantityOrdered WHERE product_id = @ProductId";
			foreach (var item in Cart)
			{
				await _db.SaveData(updateStockSql, new { QuantityOrdered = item.Quantity, ProductId = item.ProductId });
			}

			Cart.Clear(); // Clear the cart after successful order



		}

		// Get User by Username
		public async Task<UsersModel> GetUserByUsername(string username)
		{
			string sql = "SELECT * FROM Users WHERE user_name = @Username"; // Corrected to 'user_name'
			var result = await _db.LoadData<UsersModel, dynamic>(sql, new { Username = username });
			return result.FirstOrDefault(); // Return the first matching user
		}

		// Checkout
		public async Task<OrderModel> Checkout(int userId, int supplierId)
		{
			await SaveOrder(userId, supplierId); // Pass both userId and supplierId
			return new OrderModel { OrderId = 123 }; // Example, return the actual order model
		}

	}
}
