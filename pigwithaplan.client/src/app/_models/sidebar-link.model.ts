export interface SidebarLink {
  label: string;
  path?: string;
  hide?: boolean;
  roles?: string[];
  children?: SidebarLink[];
  action?: () => void;
}
