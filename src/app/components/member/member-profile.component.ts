import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { CommonModule } from '@angular/common';
interface MemberActivity {
  date: string;
  title: string;
  note: string;
}

interface Member {
  id: number;
  name: string;
  email: string;
  phone: string;
  registered: string;
  membershipType: string;
  staff: string;
  startDate: string;
  status: string;
  nextCheckIn: string;
  activities: MemberActivity[];
}

@Component({
  selector: 'app-member-profile',
  standalone: true,
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.css'],
   imports: [CommonModule]
})

export class MemberProfileComponent implements OnInit {
  member: Member | null = null;

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
