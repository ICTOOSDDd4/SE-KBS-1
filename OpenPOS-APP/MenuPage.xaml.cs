using OpenPOS_APP.Resources.Controls;
using OpenPOS_Controllers;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_APP;

public partial class MenuPage : ContentPage
{
	public List<Product> Products { get; set; }
	private HorizontalStackLayout _horizontalLayout;
	private ProductController _productController;
	private CategoryController _categoryController;
	private OrderController _orderController;
	public Dictionary<int, int> SelectedProducts { get; set; }
	public delegate void OnSearchEventHandler(object source, EventArgs args);
	public static event OnSearchEventHandler Searched;
	private int _ProductCardViewWidth = 300;
	
	private bool _isInitialized;
	private double _width;
    public MenuPage()
	{
      SelectedProducts = new Dictionary<int, int>();
      Products = _productController.GetAllProducts();
		InitializeComponent();
		Header.Searched += OnSearch;
		Header.currentPage = this;
	}

	protected override void OnSizeAllocated(double width, double height)
	{ // Gets called by MAUI
		base.OnSizeAllocated(width, height);
		if (!_isInitialized)
		{
			_isInitialized = true;
			SetWindowScaling(width,height);
		}
    }

	private void SetWindowScaling(double width, double height)
	{
		ScrView.HeightRequest = height - _ProductCardViewWidth;
		_width = width;
		AddAllCategories(_categoryController.GetAll());
      AddAllProducts();
    }

	public void AddAllProducts()
	{
		MainVerticalLayout.Clear();
      _horizontalLayout = null;
		for (int i = 0; i < Products.Count; i++)
		{
            AddProductToLayout(Products[i]);
		}
	}

   public void AddProductToLayout(Product product)
   {
	   int moduloNumber = ((int)_width / _ProductCardViewWidth);
	   if (_horizontalLayout == null || _horizontalLayout.Children.Count % moduloNumber == 0) 
		{
			AddHorizontalLayout();
		}
        
		ProductView productView = new ProductView();
		productView.SetProductValues(this,product);
		productView.ClickedMoreInfo += OnInfoButtonClicked;
      _horizontalLayout.Add(productView);
    }

    public void AddAllCategories(List<Category> categories)
    {
		//adds an "all" category
        CategoryView categoryView = new CategoryView();
        categoryView.SetCategoryValues(this, new Category() { Id = 0, Name = "All"});
        CategoryHorizontal.Add(categoryView);

        for (int i = 0; i < categories.Count; i++)
        {
            AddCategoryToLayout(categories[i]);
        }
    }

    public void AddCategoryToLayout(Category category)
    {
        CategoryView categoryView = new CategoryView();
		categoryView.SetCategoryValues(this, category);
        CategoryHorizontal.Add(categoryView);
    }

    private void AddHorizontalLayout()
	{
      HorizontalStackLayout hLayout = new HorizontalStackLayout();
		hLayout.Spacing = 20;
		hLayout.Margin = new Thickness(10);
		MainVerticalLayout.Add(hLayout);
		_horizontalLayout = hLayout;
    }

	private async void OnInfoButtonClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Work In Progress", "This will display more about the product and allergy information",
			"Understood");
	}

	private async void OrderButton_OnClicked(object sender, EventArgs e)
	{
		if (SelectedProducts.Count == 0)
		{
			await DisplayAlert("No products selected", "You forgot to add products to your order!", "Back");

		}
		else
		{
			if (await DisplayAlert("Confirm order", "Are you sure you want to place your order?", "Yes", "No"))
			{
				_orderController.CreateOrder(SelectedProducts);
			}
			else
			{
				await DisplayAlert("Order Placed", "Your order was successfully sent to our staff!", "Thank you");
				await Shell.Current.GoToAsync(nameof(MenuPage));
			}
		}		
	}

	public virtual void OnSearch(object sender, EventArgs e) {
		MainVerticalLayout.Clear();
		if (String.IsNullOrWhiteSpace(((SearchBar)sender).Text) || String.IsNullOrEmpty(((SearchBar)sender).Text))
		{
			Products = _productController.GetAllProducts();
		} else
		{
            Products = _productController.GetProductsBySearch(((SearchBar)sender).Text);
        }
		AddAllProducts();
	}
}
