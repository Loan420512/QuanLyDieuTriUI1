import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface MemberBE {
  memberId: number;
  userId: number;
  phoneNumber: string;
  name: string;
  gender: string;
}

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
  
  imports: [CommonModule],
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.css']
})
export class MemberProfileComponent implements OnInit {
  member!: Member;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const memberId = 1;

    this.http.get<MemberBE>(`/api/members/${memberId}`).subscribe(beData => {
      this.member = this.mapBackendToFrontend(beData);

      // Gọi tiếp API để lấy activities
      this.http.get<MemberActivity[]>(`https://localhost:7240/api/Member/${memberId}/activities`)
        .subscribe(activityData => {
          this.member.activities = activityData;
        });
    });
  }

  mapBackendToFrontend(be: MemberBE): Member {
    return {
      id: be.memberId,
      name: be.name,
      email: '', // Nếu cần, thêm vào từ backend
      phone: be.phoneNumber,
      registered: '',
      membershipType: '',
      staff: '',
      startDate: '',
      status: '',
      nextCheckIn: '',
      activities: [] // sẽ gán sau từ API thứ 2
    };
  }
}