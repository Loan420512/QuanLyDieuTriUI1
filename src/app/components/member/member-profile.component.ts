import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-member-profile',
  standalone: true,
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.css'],
   imports: [CommonModule]
})
export class MemberProfileComponent implements OnInit {
  member: any = null;

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    const memberId = Number(this.route.snapshot.paramMap.get('id'));
    if (memberId) {
      this.apiService.getMemberById(memberId).subscribe({
        next: (data) => this.member = data,
        error: (err) => console.error('Error loading member:', err)
      });
    }
  }
}
