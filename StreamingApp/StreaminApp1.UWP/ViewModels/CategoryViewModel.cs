using StreamingApp.Domain;
using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingApp.UWP.ViewModels
{
    public class CategoryViewModel : BindableBase
    {
        private IUnitOfWork _uow;

        public CategoryViewModel()
        {
            _uow = new UnitOfWork();
            Categories = new ObservableCollection<Category>();
            Category = new Category();
        }
        public ObservableCollection<Category> Categories { get; set; }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                Set(ref _categoryName, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        internal async void DeleteAsync()
        {
            _uow.CategoryRepository.Delete(Category);
            Categories.Remove(Category);
            await _uow.SaveAsync();
        }
        public async void LoadAllAsync()
        {
            var list = await _uow.CategoryRepository.FindAllAsync();
            Categories.Clear();

            foreach (var category in list)
            {
                Categories.Add(category);
            }
        }
        public Category _category;
        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                CategoryName = _category?.Name;
            }
        }
        //  public string PageTile { get { return Category.Id == 0 ? "New Category :
        //  Edit Category" + CategoryName; } }
        public string PageTitle
        {
            get
            {
                return Category.Id == 0 ? "New Category "
                                        : "Edit Category: " + Category.Name;
            }
        }
        public bool Valid
        {
            get { return !string.IsNullOrWhiteSpace(CategoryName); }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        public async Task<bool> CreateOrUpdateCategoryAsync()
        {
            var existingCategory =
                Categories.FirstOrDefault(x => x.Name == CategoryName);
            if (existingCategory != null)
            {
                // already exists
                // TODO: alert user
                return false;
            }
            else
            {
                if (Category.Id == 0)
                {
                    // create new category
                    var category = new Category { Name = CategoryName };
                    _uow.CategoryRepository.Create(category);
                    Categories.Add(category);
                }
                else
                {
                    // edit category
                    Category.Name = CategoryName;
                    _uow.CategoryRepository.Update(Category);
                }
                await _uow.SaveAsync();
                return true;
            }
        }
    }
}