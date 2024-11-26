namespace ProjectManagerApi.Domain.Models.CommonResources
{
    public static class Messages
    {
        #region Project messages
        public static string ProjectNotFound = "Projeto não encontrado";
        public static string ProjectHasPendingTasks = "Não é possível excluir um projeto que ainda tenham tarefas pendentes";
        public static string ProjectDisabled = "Projeto excluído com sucesso";
        #endregion

        #region Task messages
        public static string TaskNotCanBeAdded = "Não é possível adicionar uma tarefa a um projeto já tenha 20 terefas";
        public static string TaskCreated = "Tarefa criada com sucesso";
        public static string TaskNotFound = "Tarefa não encontrada";
        public static string TaskUpdated = "Tarefa atualizada com sucesso";
        public static string TaskDeleted = "Tarefa deletada com sucesso";
        public static string CommentAdded = "Comentário adicionado com sucesso";
        #endregion

        #region
        public static string UserNotFound = "Usuário não encontrado";
        public static string PositionHasNoAccess = "Usuário não tem permissão para acessar este recurso";
        #endregion
    }
}
