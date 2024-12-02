import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../models/category';
import { Post } from '../../models/post';
import { ApiService } from '../../services/api.service';
import bootstrap, { Modal } from 'bootstrap';


@Component({
  selector: 'app-post-form',
  standalone: false,
  
  templateUrl: './post-form.component.html',
  styleUrl: './post-form.component.css'
})
export class PostFormComponent implements OnInit {
  post: Post = new Post();
  categories: Category[] = [];
  isEditMode: boolean = false;
  newCategory: Category = new Category();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private apiService: ApiService,
  ) { }

  ngOnInit(): void {
    // Fetch all categories
    this.apiService.getCategories().subscribe((data) => {
      this.categories = data;
    });

    // Check if editing
    const postId = this.route.snapshot.paramMap.get('id');
    if (postId) {
      this.isEditMode = true;
      this.apiService.getPostById(+postId).subscribe((data) => {
        this.post = data;
      });
    }
  }

  submitForm(): void {
    if (this.isEditMode) {
      this.apiService.updatePost(this.post.id!, this.post).subscribe(() => {
        this.router.navigate(['/posts']);
      });
    } else {
      this.apiService.createPost(this.post).subscribe(() => {
        this.router.navigate(['/posts']);
      });
    }
  }

  deletePost(): void {
    if (confirm('Are you sure you want to delete this post?')) {
      this.apiService.deletePost(this.post.id!).subscribe(() => {
        this.router.navigate(['/posts']);
      });
    }
  }

  openCategoryDialog(): void {
    const modalElement = document.getElementById('addCategoryModal')!;
    const modal = new Modal(modalElement);
    modal.show();
  }

  addCategory(): void {
    this.apiService.createCategory(this.newCategory).subscribe((data) => {
      if (data != null) {
        this.categories.push(data); // Update categories list
        this.newCategory = new Category(); // Reset the form
      }

      const modalElement = document.getElementById('addCategoryModal')!;
      const modal = Modal.getInstance(modalElement); // Get the modal instance
      if (modal) {
        modal.hide();
      }
    });
  }
}
