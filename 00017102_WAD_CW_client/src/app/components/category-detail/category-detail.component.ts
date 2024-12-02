import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../models/category';
import { Post } from '../../models/post';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-category-detail',
  standalone: false,
  
  templateUrl: './category-detail.component.html',
  styleUrl: './category-detail.component.css'
})
export class CategoryDetailComponent implements OnInit {
  category: Category = { id: undefined, name: '' };
  posts: Post[] = [];
  isEditMode: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    const categoryId = +this.route.snapshot.paramMap.get('id')!;
    this.fetchCategoryWithPosts(categoryId);
  }

  fetchCategoryWithPosts(id: number): void {
    this.apiService.getCategoryWithPosts(id).subscribe((data) => {
      this.category = data;
      this.posts = data.posts || [];
    });
  }

  toggleEditMode(): void {
    this.isEditMode = !this.isEditMode;
  }

  saveCategoryChanges(): void {
    if (this.category) {
      this.apiService.updateCategory(this.category.id!, this.category).subscribe(() => {
        this.isEditMode = false;
      });
    }
  }

  deleteCategory(): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this.apiService.deleteCategory(this.category!.id!).subscribe(() => {
        this.router.navigate(['/categories']);
      });
    }
  }
}
