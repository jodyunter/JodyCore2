interface ITeam {
  identifier: string;
  name: string;
  skill: number;
}

type TeamState = {
  teams: ITeam[]
}

type TeamAction = {
  type: string
  team: ITeam
}

type TeamListAction = {
  type: string
  teams: Team[]
}

type DispatchType = (args: TeamAction) => TeamAction
type TeamDispatchType = (args: TeamListAction) => TeamListAction

