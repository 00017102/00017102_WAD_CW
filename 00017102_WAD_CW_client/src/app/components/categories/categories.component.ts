import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../../models/category';
import { ApiService } from '../../services/api.service';
import bootstrap, { Modal } from 'bootstrap';

@Component({
  selector: 'app-categories',
  standalone: false,
  
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css'
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  currentCategory: Category = {id: undefined, name: ''};
  isEditMode: boolean = false;

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories(): void {
    this.apiService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }

  openCategoryDialog(): void {
    this.isEditMode = false;
    this.currentCategory = new Category();
    const modalElement = document.getElementById('categoryModal')!;
    const modal = new Modal(modalElement);
    modal.show();
  }

  editCategory(category: Category): void {
    this.isEditMode = true;
    this.currentCategory = category;
    const modalElement = document.getElementById('categoryModal')!;
    const modal = new Modal(modalElement); // Get the modal instance
    if (modal) {
      modal.show();
    }
  }

  saveCategory(): void {
    if (this.isEditMode) {
      this.apiService
        .updateCategory(this.currentCategory.id!, this.currentCategory)
        .subscribe(() => {
          this.fetchCategories();
          this.closeModal();
        });
    } else {
      this.apiService.createCategory(this.currentCategory).subscribe(() => {
        this.fetchCategories();
        this.closeModal();
      });
    }
  }

  deleteCategory(id: number): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this.apiService.deleteCategory(id).subscribe(() => {
        this.fetchCategories();
      });
    }
  }

  navigateToCategory(id: number): void {
    this.router.navigate(['/category', id]);
  }

  closeModal(): void {
    const modalElement = document.getElementById('categoryModal')!;
    const modal = Modal.getInstance(modalElement); // Get the modal instance
    if (modal) {
      modal.hide();
    }
  }
}
