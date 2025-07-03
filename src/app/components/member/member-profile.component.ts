import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface MemberActivity {
  date: string;
  title: string;
  note: string;
}

interface Member {
  id: number;
  name: string;
  phone: string;
  gender: string;
  startDate: string;
  status: string;
  avatarUrl?: string;
  activities: MemberActivity[];
}

@Component({
  selector: 'app-member-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.css']
})
export class MemberProfileComponent implements OnInit {
  member: Member = {
    id: 0,
    name: '',
    phone: '',
    gender: '',
    startDate: '',
    status: '',
    avatarUrl: '',
    activities: []
  };

  editing: boolean = false;
  @ViewChild('avatarInput') avatarInputRef!: ElementRef<HTMLInputElement>;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser');
    const currentUser = userJson ? JSON.parse(userJson) : null;
    const userId = currentUser?.userId;

    if (!userId) {
      console.warn('❌ Không tìm thấy userId trong localStorage!');
      return;
    }

    // Lấy tất cả thành viên, tìm thành viên tương ứng với userId
    this.http.get<any[]>('https://localhost:7240/api/Member').subscribe(members => {
      const member = members.find(m => m.userId === userId);

      if (member) {
        this.member = {
          id: member.memberId,
          name: member.name,
          phone: member.phoneNumber,
          gender: member.gender || '',
          startDate: '2024-01-01',
          status: 'Đang hoạt động',
          avatarUrl: member.avatarUrl || '',
          activities: []
        };

        // Lấy lịch sử hoạt động
        this.http.get<MemberActivity[]>(`https://localhost:7240/api/Member/${member.memberId}/activities`)
          .subscribe(data => this.member.activities = data);
      } else {
        console.log('✅ Chưa có thông tin thành viên, cần tạo mới');
      }
    });
  }

  saveChanges() {
    const userJson = localStorage.getItem('currentUser');
    const userId = userJson ? JSON.parse(userJson)?.userId : null;

    if (!userId) {
      alert('Không tìm thấy thông tin người dùng!');
      return;
    }

    const payload = {
      userId: userId,
      name: this.member.name,
      phoneNumber: this.member.phone,
      gender: this.member.gender,
      avatarUrl: this.member.avatarUrl
    };

    const isNew = this.member.id === 0;
    const apiUrl = isNew
      ? 'https://localhost:7240/api/Member/create-member'
      : `https://localhost:7240/api/Member/${this.member.id}`;

    const request = isNew
      ? this.http.post(apiUrl, payload)
      : this.http.put(apiUrl, payload);

    request.subscribe({
      next: () => {
        this.editing = false;
        alert('✅ Lưu thông tin thành công!');
        this.ngOnInit();
      },
      error: err => {
        console.error('❌ Lỗi khi lưu:', err);
        alert('Lưu thất bại. Vui lòng thử lại.');
      }
    });
  }
  

  cancelEdit() {
    this.editing = false;
    this.ngOnInit(); // Khôi phục lại dữ liệu gốc
  }

  onAvatarChange(event: Event) {
  const input = event.target as HTMLInputElement;
  if (!input.files?.length) return;

  const file = input.files[0];
  const formData = new FormData();
  formData.append('file', file);

  this.http.post<any>('https://localhost:7240/api/upload', formData).subscribe({
    next: (res) => {
      this.member.avatarUrl = res.url; // gán đường dẫn ảnh mới
    },
    error: () => {
      alert('Tải ảnh không thành công.');
    }
  });
}


  triggerAvatarInput() {
    this.avatarInputRef.nativeElement.click();
  }
}
