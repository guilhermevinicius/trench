import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { IUser } from "app/core/models";

@Component({
  selector: 'app-profile-edit-profile-modal',
  templateUrl: './edit-profile-modal.component.html',
  imports: [
    FormsModule,
    ReactiveFormsModule
  ]
})
export class EditProfileModalComponent implements OnChanges {
  @Input({ required: true }) isOpen: boolean = false;
  @Input({ required: true }) user: IUser | null = null;

  profileForm = new FormGroup({
    firstName: new FormControl<string | null>(null, [Validators.required]),
    lastName: new FormControl<string | null>(null, [Validators.required]),
    bio: new FormControl<string | null>(null),
    isPublic: new FormControl<boolean | null>(null)
  })

  ngOnChanges(): void {
    if (this.user)
      this.profileForm.setValue({
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        bio: this.user.bio,
        isPublic: false
      })
  }

  onSubmitForm() {
    console.log("submited")
  }

}
