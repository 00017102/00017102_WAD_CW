import { Component } from '@angular/core';
import { Category } from '../../models/category';
import { ApiService } from '../../services/api.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-category-dialog',
  standalone: false,
  
  templateUrl: './add-category-dialog.component.html',
  styleUrls: ['./add-category-dialog.component.css']
})
export class AddCategoryDialogComponent {
  categoryName: string = '';

  constructor(
    public dialogRef: MatDialogRef<AddCategoryDialogComponent>,
    private apiService: ApiService
  ) { }

  addCategory(): void {
    if (this.categoryName.trim()) {
      const category: Category = { name: this.categoryName };
      this.apiService.createCategory(category).subscribe(() => {
        this.dialogRef.close(true); // Pass success back
      });
    }
  }

  closeDialog(): void {
    this.dialogRef.close(false); // Pass cancellation
  }

}
