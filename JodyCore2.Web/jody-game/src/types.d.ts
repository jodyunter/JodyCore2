interface Team {
  identifier: string;
  name: string;
  skill: number;
}

class DropdownMenu {
  dropdownMenu: HTMLSelectElement;
  options: HTMLOptionsCollection;

  constructor(dropdown: HTMLSelectElement) {
    this.dropdownMenu = dropdown;
    this.options = dropdown.options;
  }

  OnChange() {
    alert(this.options[this.options.selectedIndex].value);
    document.location.href = this.options[this.options.selectedIndex].value;
  }

}