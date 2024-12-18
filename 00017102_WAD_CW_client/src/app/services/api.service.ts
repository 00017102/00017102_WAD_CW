import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from '../models/post';
import { Category } from '../models/category';
import { CommentModel } from '../models/comment-model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5062/api' // api base url

  constructor(private http: HttpClient) { }

  // POSTS
  getPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.baseUrl}/posts`);
  }

  getPostById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.baseUrl}/posts/${id}`);
  }

  createPost(post: Post): Observable<Post | null> {
    return this.http.post<Post | null>(`${this.baseUrl}/posts`, post);
  }

  updatePost(id: number, post: Post): Observable<Post | null> {
    return this.http.put<Post | null>(`${this.baseUrl}/posts/${id}`, post);
  }

  deletePost(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/posts/${id}`);
  }

  // CATEGORIES
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseUrl}/categories`);
  }

  getCategoryById(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.baseUrl}/categories/${id}`);
  }

  getCategoryWithPosts(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.baseUrl}/categories/filter/${id}`);
  }

  createCategory(category: Category): Observable<Category | null> {
    return this.http.post<Category | null>(`${this.baseUrl}/categories`, category);
  }

  updateCategory(id: number, category: Category): Observable<Category | null> {
    return this.http.put<Category | null>(`${this.baseUrl}/categories/${id}`, category);
  }

  deleteCategory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/categories/${id}`);
  }

  // COMMENTS
  getComments(): Observable<CommentModel[]> {
    return this.http.get<CommentModel[]>(`${this.baseUrl}/comments`);
  }

  getCommentById(id: number): Observable<CommentModel> {
    return this.http.get<CommentModel>(`${this.baseUrl}/comments/${id}`);
  }

  createComment(comment: CommentModel): Observable<CommentModel | null> {
    return this.http.post<CommentModel | null>(`${this.baseUrl}/comments`, comment);
  }
}
